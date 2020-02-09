using System;
using Readr.DataObjects;
using Readr.Repositories.Interfaces;

namespace Services
{
    public class AppUserService : IAppUserService, IDisposable
    {
        private IAppUserRepository _appUserRepo;

        public AppUserService(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        public AppUser AddAppUser(string username)
        {
            try
            {
                var existingAppUser = _appUserRepo.GetAppUser(username);

                //Check if the username already exists
                if (existingAppUser == null)
                {
                    //if the appUser does not exist, create it
                    var newAppUser = new AppUser() { Username = username };
                    var appUserResult = _appUserRepo.AddAppUser(newAppUser);
                    if (appUserResult != null)
                    {
                        _appUserRepo.Save();

                        //adding appUser was successful
                        return appUserResult;
                    }
                }

                //adding appUser failed
                return null;
            }
            catch
            {

            }
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {                    
                    //_appUserRepo.Dispose()
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
