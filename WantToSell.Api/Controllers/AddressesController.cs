using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Address.Commands;
using WantToSell.Application.Features.Address.Models;
using WantToSell.Application.Features.Address.Queries;

namespace WantToSell.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/address")]
public class AddressController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AddressDetailModel>> Get()
    {
        var result = await _mediator.Send(new GetAddress.Query());

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(AddressCreateModel model)
    {
        await _mediator.Send(new CreateAddress.Command(model));

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(AddressUpdateModel model)
    {
        var result = await _mediator.Send(new UpdateAddress.Command(model));

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
            await _mediator.Send(new DeleteAddress.Command(id));

        return NoContent();
    }
}