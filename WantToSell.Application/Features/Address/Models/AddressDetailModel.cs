﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WantToSell.Application.Features.Address.Models
{
	public class AddressDetailModel
	{
		public Guid Id { get; set; }
		public string ApartmentNumber { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string PostalCode { get; set; }
	}
}
