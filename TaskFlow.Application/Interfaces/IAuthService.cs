namespace TaskFlow.Application.Interfaces;

public interface IAuthService
{
    Task<List<string>> GetUserRolesAsync(Guid userId);
}
