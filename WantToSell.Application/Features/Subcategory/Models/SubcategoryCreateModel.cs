﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WantToSell.Application.Features.Subcategory.Models
{
	public class SubcategoryCreateModel
	{
		public string Name { get; set; }
		public Guid CategoryId { get; set; }
	}
}
