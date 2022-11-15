using Moasher.Application.Common.Constants;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Common.Types;
using Moasher.Application.Features.UserNotifications.Queries;
using Moasher.Domain.Entities;
using Moasher.Domain.Types;

namespace Moasher.Application.Features.UserNotifications;

public class UserNotificationService : IUserNotification
{
    private readonly IMoasherDbContext _context;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IMailService _mailService;

    public UserNotificationService(IMoasherDbContext context, IEmailTemplateService emailTemplateService,
        IMailService mailService)
    {
        _context = context;
        _emailTemplateService = emailTemplateService;
        _mailService = mailService;
    }

    public async Task<UserNotification> CreateAsync(string title, string body, User user, CancellationToken cancellationToken = new())
    {
        var notification = new UserNotification
        {
            Title = title,
            Body = body,
            CreatedAt = LocalDateTime.Now,
            UserId = user.Id
        };

        _context.UserNotifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);

        return notification;
    }

    public async Task<UserNotification> CreateAsync(string title, string body, IEnumerable<User> users, CancellationToken cancellationToken = new())
    {
        var notifications = users.Select(user => new UserNotification
        {
            Title = title, 
            Body = body, 
            CreatedAt = LocalDateTime.Now, 
            UserId = user.Id
        }).ToList();
        
        _context.UserNotifications.AddRange(notifications);
        await _context.SaveChangesAsync(cancellationToken);
        return notifications.FirstOrDefault() ?? new UserNotification
        {
            Title = title, 
            Body = body, 
            CreatedAt = LocalDateTime.Now, 
            UserId = Guid.Empty
        };
    }

    public Task NotifyAsync(UserNotification notification, User user, CancellationToken cancellationToken = new())
    {
        return SendAsync(notification, new List<User> {user}, cancellationToken);
    }

    public Task NotifyAsync(UserNotification notification, IEnumerable<User> users, CancellationToken cancellationToken = new())
    {
        return SendAsync(notification, users, cancellationToken);
    }

    private Task SendAsync(UserNotification notification, IEnumerable<User> users, CancellationToken cancellationToken = new())
    {
        var emails = users
            .Where(u => u.ReceiveEmailNotification)
            .Select(u => u.Email)
            .ToList();
        if (!emails.Any())
        {
            return Task.CompletedTask;
        }
        var emailModel = new CreateUserNotificationEmailModel(notification.Title, notification.Body);
        var emailTemplate = _emailTemplateService.GenerateEmailTemplate(EmailTemplates.UserNotification, emailModel);
        var mailRequest = new MailRequest(emails, notification.Title, emailTemplate);
        return _mailService.SendAsync(mailRequest, cancellationToken);
    }
}