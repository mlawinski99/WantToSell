namespace WantToSell.Application.Models.Paging;

public class PagedList<T>
{
    public List<T> Items { get; set; } = new();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public bool IsPreviousPage => PageIndex > 1;
    public bool IsNextPage => PageIndex * PageSize < TotalRecords;
    public string SortColumn { get; set; }
    public bool Ascending { get; set; }
    
    public PagedList(int pageIndex, int pageSize, string sortColumn, bool ascending, int totalRecords)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        SortColumn = sortColumn;
        Ascending = ascending;
        TotalRecords = totalRecords;
    }
    
    public PagedList(List<T> items, int pageIndex, int pageSize, string sortColumn, bool ascending, int totalRecords)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        SortColumn = sortColumn;
        Ascending = ascending;
        TotalRecords = totalRecords;
    }
}