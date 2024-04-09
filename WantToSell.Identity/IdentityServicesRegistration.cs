using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WantToSell.Application.Contracts.Identity;
using WantToSell.Application.Models.Identity;
using WantToSell.Domain.Settings;
using WantToSell.Identity.DbContexts;
using WantToSell.Identity.Models;
using WantToSell.Identity.Services;

namespace WantToSell.Identity;

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
        services.AddScoped<IUserContext, UserContext>();

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

        //services.AddDatabaseMigrationServices();

        return services;
    }

    private static void AddDatabaseMigrationServices(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<WantToSellIdentityContext>();

        ApplyMigrations(dbContext);
    }

    private static void ApplyMigrations(WantToSellIdentityContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        dbContext.Database.Migrate();
    }
}