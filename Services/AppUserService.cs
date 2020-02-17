using System;
using System.Threading.Tasks;
using Readr.Models;
using Readr.Repositories.Interfaces;
using Readr.Services.Interfaces;

namespace Readr.Services
{
    public class AppUserService : IAppUserService, IDisposable
    {
        private IAppUserRepository _appUserRepo;

        public AppUserService(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        public async Task<AppUserDto> AddAppUserAsync(string username)
        {
            try
            {
                var existingAppUser = await _appUserRepo.GetAppUserByUsernameAsync(username).ConfigureAwait(false);

                //Check if the username already exists
                if (existingAppUser == null)
                {
                    //if the appUser does not exist, create it
                    return await _appUserRepo.AddAppUserAsync(AppUser.CreateUserAsync(username).Result).ConfigureAwait(false);
                }

                //adding appUser failed
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AppUserDto> LoginAppUserAsync(string username)
        {
            try
            {
                //return the AppUser if the AppUserExists. 
                var existingAppUser = await _appUserRepo.GetAppUserByUsernameAsync(username).ConfigureAwait(false);
                return existingAppUser;
            }
            catch (Exception ex)
            {
                throw;
            }
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
