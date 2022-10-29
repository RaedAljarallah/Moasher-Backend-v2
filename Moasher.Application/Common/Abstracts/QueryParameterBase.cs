namespace Moasher.Application.Common.Abstracts;

public abstract record QueryParameterBase
{
    private int _pageSize = 10;
    private int _pageNumber = 1;
    private string? _searchQuery;
    private const int MaxPageSize = 50;
    
    public int PageNumber
    {
        get => IgnorePagination ? 1 : _pageNumber;
        set => _pageNumber = (value <= 0) ? _pageNumber : value;
    }

    public int PageSize
    {
        get => IgnorePagination ? -1 : _pageSize;
        set => _pageSize = (value <= 0) ? _pageSize : (value > MaxPageSize) ? _pageSize : value;
    }

    public string? SearchQuery { get => _searchQuery; set => _searchQuery = value?.Trim(); }

    public string OrderBy { get; set; } = "Name";
    public bool IgnorePagination { get; set; }
    public Guid? Id { get; set; }
}