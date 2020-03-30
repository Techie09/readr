using System.Threading.Tasks;
using Readr.Models;

namespace Readr.Services.Interfaces
{
    public interface IUserSessionService
    {
        Task<UserSessionDto> UpsertUserSessionAsync(UserSession userSession);
    }
}
