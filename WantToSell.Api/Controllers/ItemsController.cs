using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Items.Commands;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Application.Features.Items.Queries;

namespace WantToSell.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetItem.Query(id));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetItemList.Query());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]ItemCreateModel model)
    {
        var result = await _mediator.Send(new CreateItem.Command(model));

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm]ItemUpdateModel model)
    {
        var result = await _mediator.Send(new UpdateItem.Command(model));

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteItem.Command(id));
        return NoContent();
    }
}