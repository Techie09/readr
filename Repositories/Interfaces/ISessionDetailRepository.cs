using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Readr.Models;

namespace Readr.Repositories.Interfaces
{
    public interface ISessionDetailRepository
    {
        Task<SessionDetail> UpsertSessionDetail(SessionDetail sessionDetail);
        IAsyncEnumerable<SessionDetail> GetSessionDetailByUserSessionId(ObjectId userSessionId);
        IAsyncEnumerable<SessionDetail> GetUserSessionByAppUserUserId(ObjectId appUserId);
        Task<SessionDetail> GetSessionDetailBySessionDetailId(ObjectId sessionDetailId);
    }
}
