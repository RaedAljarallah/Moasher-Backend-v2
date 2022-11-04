namespace Moasher.Application.Common.Types;

public record MailRequest(List<string> To, string Subject, string? Body = null, string? From = null,
    string? DisplayName = null, string? ReplyTo = null, string? ReplyToName = null, List<string>? Bcc = null,
    List<string>? Cc = null, IDictionary<string, byte[]>? AttachmentData = null,
    IDictionary<string, string>? Headers = null);