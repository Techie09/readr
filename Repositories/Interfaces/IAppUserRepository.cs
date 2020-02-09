using System;
using System.Collections.Generic;
using System.Text;
using Readr.DataObjects;

namespace Readr.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        IEnumerable<AppUser> GetAppUsers();
        AppUser GetAppUser(int appUserId);
        AppUser GetAppUser(string username);
        AppUser AddAppUser(AppUser appUser);
        void Save();
    }
}
