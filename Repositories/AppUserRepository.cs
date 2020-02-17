using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Readr.Models;
using Readr.Repositories.Interfaces;

namespace Readr.Repositories
{
    /// <summary>
    /// a disposable repository that manages AppUsers
    /// </summary>
    public class AppUserRepository : IAppUserRepository, IDisposable
    {
        /// <summary>
        /// Defines the context used by the repository to make changes to a datastore
        /// </summary>
        private MongoClient _client;

        private IMongoDatabase _db;
        private IMongoCollection<AppUser> _users;

        /// <summary>
        /// Initializes the AppUserRepository with a given AppUserContext
        /// </summary>
        /// <param name="context"></param>
        public AppUserRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("Readr");
            _users = _db.GetCollection<AppUser>("Users");
        }

        #region IAppUserRepository Support
        /// <summary>
        /// returns a list of all appUsers from the repository.
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<AppUser> GetAppUsersAsync()
        {
            using (IAsyncCursor<AppUser> cursor = await _users.FindAsync(new BsonDocument()))
            {
                IEnumerable<AppUser> batch = cursor.Current;
                foreach (AppUser document in batch)
                {
                    yield return document;
                }
            }

            //return _users.Find(new BsonDocument())();
        }

        /// <summary>
        /// returns the first appUser found that matches the AppuserId
        /// if no appUser is found, default value of appUser is returned instead.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppUserDto> GetAppUserByIdAsync(string id)
        {
            return await await _users.Find(u => u.Id == ObjectId.Parse(id)).Project(u => u.MapToDto()).SingleOrDefaultAsync();
        }

        /// <summary>
        /// returns the first appUser found that matches the username exactly (case sensitive)
        /// if no appUser is found, default value of AppUser is returned instead. 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AppUserDto> GetAppUserByUsernameAsync(string username)
        {
            return await await _users.Find(u => u.Username == username).Project(u => u.MapToDto()).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Add an AppUser to the AppUser Repository
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns>returns an instance of the AppUser that was added to the repository</returns>
        public async Task<AppUserDto> AddAppUserAsync(AppUser appUser)
        {
            await _users.InsertOneAsync(appUser);
            return await appUser.MapToDto();
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
