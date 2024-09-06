using Microsoft.AspNetCore.Http;

namespace WantToSell.Application.Features.Items.Models;

public class ItemDetailModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateExpiredUtc { get; set; }
    public string Condition { get; set; }
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public List<IFormFile> Images { get; set; }
}