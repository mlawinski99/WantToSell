namespace WantToSell.Application.Models.Paging;

public class Pager
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 20;
    public string SortColumn { get; set; } = "DateCreatedUtc";
    public bool Ascending { get; set; } = true;
}