using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Readr.Models;
using Readr.Repositories.Interfaces;

namespace Readr.Repositories
{
    public class UserSessionRepository : CoreRepository<UserSession>, IUserSessionRepository
    {
        public UserSessionRepository(IMongoDbSettings settings) : base(settings)
        {
            Init("UserSessions");
        }

        public async Task<UserSession> UpsertUserSession(UserSession userSession)
        {

            var filter = Builders<UserSession>.Filter
                .Where(t => t.Id == userSession.Id);

            var update = Builders<UserSession>.Update
                .Set(nameof(UserSession.Id), userSession.Id)
                .Set(nameof(UserSession.Isbn), userSession.Isbn)
                .Set(nameof(UserSession.Description), userSession.Description)
                .Set(nameof(UserSession.ModifiedById), userSession.ModifiedById)
                .CurrentDate(nameof(UserSession.ModifiedOn));

            var options = new UpdateOptions
            {
                IsUpsert = true
            };

            await UpdateAsync(filter, update, options);
            return userSession;
        }

        public async IAsyncEnumerable<UserSession> GetUserSessionByAppUserUserId(ObjectId appUserId)
        {
            var e = Get(T => T.ModifiedById == appUserId).GetAsyncEnumerator();
            while (await e.MoveNextAsync())
            {
                yield return e.Current;
            }
        }

        public async Task<UserSession> GetUserSessionByUserSessionId(ObjectId userSessionId)
        {
            return await GetById(userSessionId);
        }
    }
}
