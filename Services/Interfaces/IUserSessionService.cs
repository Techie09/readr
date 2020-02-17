using System.Threading.Tasks;
using Readr.Models;

namespace Readr.Services.Interfaces
{
    public interface IUserSessionService
    {
        Task<UserSessionDto> CreateUserSessionAsync(string appUserId, string isbn, string description);
    }
}
