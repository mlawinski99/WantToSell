using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Queries;

namespace WantToSell.Api.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetCategory.Query(id));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetCategoryList.Query());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        await _mediator.Send(new CreateCategory.Command(model));

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateModel model)
    {
        await _mediator.Send(new UpdateCategory.Command(model));


        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteCategory.Command(id));

        return NoContent();
    }
}