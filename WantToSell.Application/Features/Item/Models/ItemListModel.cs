using Microsoft.AspNetCore.Http;

namespace WantToSell.Application.Features.Items.Models;

public class ItemListModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<IFormFile> Images { get; set; }
}