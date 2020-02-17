using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Readr.Models
{
    public class UserSession : IBsonModel, ITrackChanges
    {
        public ObjectId Id { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public ObjectId ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }

        public static async Task<UserSession> CreateUserSessionAsync(string isbn, string description, string appUserId)
        {
            var userSession = await CreateUserSession(isbn, description);
            await userSession.SetModifiedPropertiesAsync(ObjectId.Parse(appUserId), DateTime.Now);
            return userSession;
        }

        protected static Task<UserSession> CreateUserSession(string isbn, string description)
        {
            return Task.FromResult(
                new UserSession
                {
                    Id = ObjectId.GenerateNewId(),
                    Isbn = isbn,
                    Description = description
                }
            );
        }
    }

    public static partial class ExtensionMethods
    {
        public static async Task<UserSessionDto> MapToDto(this UserSession userSession)
        {
            return await Task.FromResult(new UserSessionDto()
            {
                Id = userSession.Id.ToString(),
                Isbn = userSession.Isbn,
                Description = userSession.Description,
                ModifiedById = userSession.ModifiedById.ToString(),
                ModifiedOn = userSession.ModifiedOn
            });
        }
    }
}
