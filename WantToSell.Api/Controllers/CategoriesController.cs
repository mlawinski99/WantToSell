using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Queries;

namespace WantToSell.Api.Controllers
{
	[Authorize]
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
		public async Task<List<CategoryListModel>> GetList()
		{
			return await _mediator.Send(new GetCategoryList.Query());
		}

		[HttpPost]
		public async Task<IActionResult> Create(CategoryCreateModel model)
		{
			await _mediator.Send(new CreateCategory.Command(model));
			return Accepted();
		}

		[HttpPut]
		public async Task<IActionResult> Create(CategoryUpdateModel model)
		{
			await _mediator.Send(new UpdateCategory.Command(model));
			return Accepted();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Create(Guid id)
		{
			await _mediator.Send(new DeleteCategory.Command(id));
			return NoContent();
		}
	}
}