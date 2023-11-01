using Microsoft.AspNetCore.Mvc;

namespace WantToSell.Api.Models
{
	public class CustomProblemDetails : ProblemDetails
	{
		public IDictionary<string, string[]> Errors { get; set; }
	}
}
