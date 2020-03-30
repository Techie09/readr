using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Readr.Models;
using Readr.Repositories.Interfaces;

namespace Readr.Repositories
{
    public class SessionDetailRepository : CoreRepository<SessionDetail>, ISessionDetailRepository
    {
        public SessionDetailRepository(IMongoDbSettings settings) : base(settings)
        {
            Init("SessionDetails");
        }

        public async Task<SessionDetail> UpsertSessionDetail(SessionDetail sessionDetail)
        {
            //var filter = Builders<SessionDetail>.Filter
            //    .Where(t => t.Id == sessionDetail.Id);

            //var update = Builders<SessionDetail>.Update
            //    .Set(nameof(SessionDetail.Id), sessionDetail.Id)
            //    .Set(nameof(SessionDetail.SessionId), sessionDetail.SessionId) //current session
            //    .Set(nameof(SessionDetail.PrintedTextResult), sessionDetail.PrintedTextResult) //Ocr Result
            //    .Set(nameof(SessionDetail.ModifiedById), sessionDetail.ModifiedById) //current AppUser
            //    .CurrentDate(nameof(SessionDetail.ModifiedOn));

            //var options = new UpdateOptions
            //{
            //    IsUpsert = true
            //};


            await InsertAsync(sessionDetail);
            return sessionDetail;
        }

        public async IAsyncEnumerable<SessionDetail> GetSessionDetailByUserSessionId(ObjectId userSessionId)
        {
            var e = Get(t => t.SessionId == userSessionId).GetAsyncEnumerator();
            while(await e.MoveNextAsync())
            {
                yield return e.Current;
            }
        }

        public async IAsyncEnumerable<SessionDetail> GetUserSessionByAppUserUserId(ObjectId appUserId)
        {
            var e = Get(T => T.ModifiedById == appUserId).GetAsyncEnumerator();
            while (await e.MoveNextAsync())
            {
                yield return e.Current;
            }
        }

        public async Task<SessionDetail> GetSessionDetailBySessionDetailId(ObjectId sessionDetailId)
        {
            return await GetById(sessionDetailId);
        }
    }
}
