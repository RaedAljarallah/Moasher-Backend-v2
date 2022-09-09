namespace Moasher.WebApi.Controllers.Common.ResponseTypes.Abstracts;

public class ErrorResponseSchema
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int Status { get; set; }
}