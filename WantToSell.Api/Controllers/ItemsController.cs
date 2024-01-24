using MediatR;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Items.Commands;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Queries;

namespace WantToSell.Api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<ItemListModel>> GetList()
    {
        return await _mediator.Send(new GetItemList.Query());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ItemCreateModel model)
    {
        await _mediator.Send(new CreateItem.Command(model));
        return Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> Update(ItemUpdateModel model)
    {
        await _mediator.Send(new UpdateItem.Command(model));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteItem.Command(id));
        return NoContent();
    }
}