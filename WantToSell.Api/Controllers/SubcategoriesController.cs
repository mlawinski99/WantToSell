using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Subcategory.Commands;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Queries;

namespace WantToSell.Api.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/subcategories")]
public class SubcategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubcategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetSubcategory.Query(id));

        return Ok(result);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetList(Guid categoryId)
    {
        //Get Subcategories for Category by Id
        var result = await _mediator.Send(new GetSubcategoryListByCategoryId.Query(categoryId));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubcategoryCreateModel model)
    {
        await _mediator.Send(new CreateSubcategory.Command(model));

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(SubcategoryUpdateModel model)
    {
        await _mediator.Send(new UpdateSubcategory.Command(model));

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSubcategory.Command(id));

        return NoContent();
    }
}