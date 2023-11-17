﻿using System;
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
			CreateMap<Subcategory, SubcategoryListModel>()
				.ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name)); ;
			CreateMap<SubcategoryCreateModel, Subcategory>();
			CreateMap<SubcategoryUpdateModel, Subcategory>();
		}
	}
}
