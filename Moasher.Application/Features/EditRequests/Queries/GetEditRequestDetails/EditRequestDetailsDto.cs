namespace Moasher.Application.Features.EditRequests.Queries.GetEditRequestDetails;

public record EditRequestDetailsDto
{
    public IEnumerable<EditRequestValue>? CurrentValues { get; set; }
    public IEnumerable<EditRequestValue>? OriginalValues { get; set; }
    public Guid EditRequestId { get; set; }
}

public record EditRequestValue
{
    public string ModelName { get; set; } = default!;

    public List<Dictionary<string, object>> Values { get; set; } = new();
}