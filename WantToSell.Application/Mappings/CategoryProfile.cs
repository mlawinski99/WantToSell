﻿using AutoMapper;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryViewModel>();
        CreateMap<CategoryCreateModel, Category>();
        CreateMap<CategoryUpdateModel, Category>();
    }
}