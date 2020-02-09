using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Readr.DataObjects;
using Readr.Repositories.Contexts;
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
        private AppUserContext _context;

        /// <summary>
        /// Initializes the AppUserRepository with a given AppUserContext
        /// </summary>
        /// <param name="context"></param>
        public AppUserRepository(AppUserContext context)
        {
            _context = context;
        }

        #region IAppUserRepository Support
        /// <summary>
        /// returns a list of all appUsers from the repository.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AppUser> GetAppUsers()
        {
            return _context.AppUsers.ToList();
        }

        /// <summary>
        /// returns the first appUser found that matches the AppuserId
        /// if no appUser is found, default value of appUser is returned instead.
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        public AppUser GetAppUser(int appUserId)
        {
            return _context.AppUsers.FirstOrDefault(au => au.AppUserId.Equals(appUserId));
        }

        /// <summary>
        /// returns the first appUser found that matches the username exactly (case sensitive)
        /// if no appUser is found, default value of AppUser is returned instead. 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public AppUser GetAppUser(string username)
        {
            return _context.AppUsers.FirstOrDefault(au => au.Username.Equals(username));
        }

        /// <summary>
        /// Add an AppUser to the AppUser Repository
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns>returns an instance of the AppUser that was added to the repository</returns>
        public AppUser AddAppUser(AppUser appUser)
        {
            _context.AppUsers.Add(appUser);
            return appUser;
        }

        /// <summary>
        /// Commits changes to the Repository within a transactionscope and try catch to handle and rollback changes if an error occurs.
        /// </summary>
        public void Save()
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    _context.SaveChanges();
                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    //TODO: handle error logging
                }
            }
            
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
                    _context.Dispose();
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
