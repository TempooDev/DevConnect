namespace DevConnect.Shared.Pagination;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; }
    public int TotalCount { get; init; }

    public PagedResult(IReadOnlyList<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
