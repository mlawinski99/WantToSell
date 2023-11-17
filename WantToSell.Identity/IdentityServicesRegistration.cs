using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Models.Identity;
using WantToSell.Identity.DbContexts;
using WantToSell.Identity.Models;
using WantToSell.Identity.Services;

namespace WantToSell.Identity
{
	public static class IdentityServicesRegistration
	{
		public static IServiceCollection AddIdentityServicesRegistration(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

			services.AddDbContext<WantToSellIdentityContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("WantToSellDbConnectionString")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<WantToSellIdentityContext>()
				.AddDefaultTokenProviders();

			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IUserService, UserService>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidIssuer = configuration["TokenSettings:Issuer"],
					ValidAudience = configuration["TokenSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSettings:Key"]))
				};
			});

			return services;
		}
	}
}
