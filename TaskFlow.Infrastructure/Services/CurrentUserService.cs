using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return id is not null ? Guid.Parse(id) : null;
        }
    }

    public string? Email
        => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

    public IEnumerable<string> Roles
        => _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];
}
