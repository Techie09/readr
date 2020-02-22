using System;
using System.Threading.Tasks;
using Readr.Models;
using Readr.Repositories.Interfaces;
using Readr.Services.Interfaces;

namespace Readr.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AppUserService : IAppUserService, IDisposable
    {
        private IAppUserRepository _appUserRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUserRepo"></param>
        public AppUserService(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        /// <summary>
        /// This Will attempt to add an <see cref="AppUser"/> by calling an instance of <see cref="IAppUserRepository"/>
        /// </summary>
        /// <param name="username"></param>
        /// <example>
        /// Usage:
        /// <code>
        /// AppUserService.AddAppUserAsync("test");
        /// </code>
        /// </example>
        /// <returns></returns>
        public async Task<AppUserDto> AddAppUserAsync(string username)
        {
            var existingAppUser = await _appUserRepo.GetAppUserByUsernameAsync(username).ConfigureAwait(false);

            //Check if the username already exists
            if (existingAppUser == null)
            {
                //if the appUser does not exist, create it
                return await _appUserRepo.AddAppUserAsync(AppUser.CreateUserAsync(username).Result).ConfigureAwait(false);
            }
            
            //adding appUser failed
            throw new ApplicationException($"User {username} already exists");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<AppUserDto> LoginAppUserAsync(string username)
        {
            //return the AppUser if the AppUserExists. 
            var existingAppUser = await _appUserRepo.GetAppUserByUsernameAsync(username).ConfigureAwait(false);
            return existingAppUser;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _appUserRepo.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppUserService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
