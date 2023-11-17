using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantToSell.Application.Features.Category.Commands;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Category.Queries;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Application.Features.Subcategory.Queries;

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

		[HttpGet]
		public async Task<List<CategoryListModel>> GetList()
		{
			return await _mediator.Send(new GetCategoryList.Query());
		}

		[HttpGet("{id}")]
		public async Task<List<SubcategoryListModel>> GetList(Guid id)
		{
			//Get Subcategories for Category by Id
			return await _mediator.Send(new GetSubcategoryList.Query(id)); 
		}

		[HttpPost]
		public async Task<IActionResult> Create(CategoryCreateModel model)
		{
			await _mediator.Send(new CreateCategory.Command(model));
			return Accepted();
		}

		[HttpPut]
		public async Task<IActionResult> Update(CategoryUpdateModel model)
		{
			await _mediator.Send(new UpdateCategory.Command(model));
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _mediator.Send(new DeleteCategory.Command(id));
			return NoContent();
		}
	}
}