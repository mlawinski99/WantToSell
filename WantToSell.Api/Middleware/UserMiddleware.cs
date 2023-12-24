using WantToSell.Domain.Interfaces;

namespace WantToSell.Api.Middleware;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserContext userContext)
    {
        var userIdClaim = context.User.FindFirst("uid")?.Value;

        userContext.UserId = Guid.Parse(userIdClaim);

        await _next(context);
    }
}