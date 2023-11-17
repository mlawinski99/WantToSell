using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WantToSell.Application.Features.Category.Models;
using WantToSell.Application.Features.Items.Models;
using WantToSell.Domain;

namespace WantToSell.Application.Mappings
{
	public class ItemProfile : Profile
	{
		public ItemProfile()
		{
			CreateMap<Item, ItemListModel>();
			CreateMap<Item, ItemDetailModel>();
			CreateMap<ItemCreateModel, Item>();
			CreateMap<ItemUpdateModel, Item>();
		}
	}
}
