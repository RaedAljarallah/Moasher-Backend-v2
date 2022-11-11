// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Moasher.Application.Common.Interfaces;
// using Moasher.Domain.Common.Abstracts;
// using Moasher.Domain.Common.Interfaces;
// using Moasher.Domain.Entities;
// using Moasher.Domain.Entities.InitiativeEntities;
// using Moasher.Domain.Enums;
// using Moasher.Domain.Types;
// using Newtonsoft.Json;
//
// namespace Moasher.Persistence.Interceptors;
//
// public class AuditingInterceptor : SaveChangesInterceptor
// {
//     private readonly ICurrentUser _currentUser;
//
//     public AuditingInterceptor(ICurrentUser currentUser)
//     {
//         _currentUser = currentUser;
//     }
//     public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
//         CancellationToken cancellationToken = new())
//     {
//         if (eventData.Context is not null)
//         {
//             HandelAuditableEntries(eventData.Context);
//             HandelApprovableEntries(eventData.Context);
//         }
//         return base.SavingChangesAsync(eventData, result, cancellationToken);
//     }
//     
//     private void HandelAuditableEntries(DbContext context)
//     {
//         foreach (var entry in context.ChangeTracker.Entries<AuditableDbEntity>())
//         {
//             switch (entry.State)
//             {
//                 case EntityState.Added:
//                     entry.Entity.CreatedBy = _currentUser.GetEmail() ?? string.Empty;
//                     entry.Entity.CreatedAt = LocalDateTime.Now;
//                     break;
//                 case EntityState.Modified:
//                     entry.Entity.LastModifiedBy = _currentUser.GetEmail() ?? "System";
//                     entry.Entity.LastModified = LocalDateTime.Now;
//                     break;
//             }
//         }
//
//         foreach (var projectProgress in context.ChangeTracker.Entries<InitiativeProjectProgress>())
//         {
//             switch (projectProgress.State)
//             {
//                 case EntityState.Added:
//                     projectProgress.Entity.PhaseStartedAt = projectProgress.Entity.CreatedAt;
//                     projectProgress.Entity.PhaseStartedBy = projectProgress.Entity.CreatedBy;
//                     break;
//                 case EntityState.Modified:
//                     projectProgress.Entity.PhaseEndedAt = projectProgress.Entity.LastModified;
//                     projectProgress.Entity.PhaseEndedBy = projectProgress.Entity.LastModifiedBy;
//                     break;
//             }
//         }
//     }
//
//     private void HandelApprovableEntries(DbContext context)
//     {
//         var approved = _currentUser.IsSuperAdmin() || _currentUser.IsAdmin();
//         foreach (var entry in context.ChangeTracker.Entries<ApprovableDbEntity>())
//         {
//             if (approved)
//             {
//                 entry.Entity.Approved = true;
//                 break;
//             }
//             
//             
//             // var currentValues = new Dictionary<string, object?>();
//             // var originalValues = new Dictionary<string, object?>();
//             // var gg = entry.CurrentValues;
//             // var ggh = entry.Properties;
//             // foreach (var property in entry.Properties)
//             // {
//             //     var propertyName = property.Metadata.Name;
//             //     switch (entry.State)
//             //     {
//             //         case EntityState.Added:
//             //             currentValues[propertyName] = property.CurrentValue;
//             //             break;
//             //         case EntityState.Modified:
//             //             currentValues[propertyName] = property.CurrentValue;
//             //             originalValues[propertyName] = property.OriginalValue;
//             //             break;
//             //         case EntityState.Deleted:
//             //             originalValues[propertyName] = property.OriginalValue;
//             //             break;
//             //     }
//             // }
//             entry.Entity.Approved = false;
//             var editRequest = entry.Entity.GetEditRequest();
//             editRequest.Model = entry.Entity.GetType().Name;
//             editRequest.RequestedBy = _currentUser.GetEmail() ?? string.Empty;
//             // editRequest.OriginalValues = JsonConvert.SerializeObject(originalValues);
//             // editRequest.CurrentValues = JsonConvert.SerializeObject(currentValues);
//             context.Set<EditRequest>().Add(editRequest);
//         }
//     }
// }