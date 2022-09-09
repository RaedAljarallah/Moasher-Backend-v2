namespace Moasher.WebApi.Controllers.Common.ResponseTypes.Abstracts;

public class ValidationErrorResponseSchema : ErrorResponseSchema
{
    public IEnumerable<string>? Errors { get; set; }
}