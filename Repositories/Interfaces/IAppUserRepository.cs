using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Readr.Models;

namespace Readr.Repositories.Interfaces
{
    public interface IAppUserRepository : IDisposable
    {
        IAsyncEnumerable<AppUser> GetAppUsersAsync();
        Task<AppUser> GetAppUserByIdAsync(string id);
        Task<AppUser> GetAppUserByUsernameAsync(string username);
        Task<AppUser> AddAppUserAsync(AppUser appUser);
    }
}
