namespace WantToSell.Application.Models.Paging;

public class Pager
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SortColumn { get; set; } = "DateCreatedUtc";
    public bool Ascending { get; set; } = true;
}