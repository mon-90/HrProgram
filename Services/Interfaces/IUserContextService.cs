using System.Security.Claims;

namespace HrProgram.Services.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}
