using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Subcategory.Commands;
using WantToSell.Application.Features.Subcategory.Models;

namespace WantToSell.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/subcategories")]
	public class SubcategoriesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SubcategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Create(SubcategoryCreateModel model)
		{
			await _mediator.Send(new CreateSubcategory.Command(model));
			return Accepted();
		}

		[HttpPut]
		public async Task<IActionResult> Update(SubcategoryUpdateModel model)
		{
			await _mediator.Send(new UpdateSubcategory.Command(model));
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _mediator.Send(new DeleteSubcategory.Command(id));
			return NoContent();
		}
	}
}
