namespace WantToSell.Application.Features.Item.Filters;

public class ItemFilter
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? CategoryName { get; set; }
    public string? SubcategoryName { get; set; }
    public string? Condition { get; set; }
}