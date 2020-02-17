using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Readr.Models;

namespace Readr.Repositories.Interfaces
{
    public interface IUserSessionRepository
    {
        Task<UserSession> UpsertUserSession(UserSession userSession);
        Task<UserSession> GetUserSessionByUserSessionId(ObjectId userSessionId);
        IAsyncEnumerable<UserSession> GetUserSessionByAppUserUserId(ObjectId appUserId);
    }
}
