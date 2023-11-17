using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WantToSell.Application.Features.Subcategory.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Mappings
{
	public class SubcategoryProfile : Profile
	{
		public SubcategoryProfile()
		{
			CreateMap<Category, SubcategoryListModel>();
			CreateMap<SubcategoryCreateModel, Category>();
			CreateMap<SubcategoryUpdateModel, Category>();
		}
	}
}
