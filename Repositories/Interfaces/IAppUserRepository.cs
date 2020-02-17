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
        Task<AppUserDto> GetAppUserByIdAsync(string id);
        Task<AppUserDto> GetAppUserByUsernameAsync(string username);
        Task<AppUserDto> AddAppUserAsync(AppUser appUser);
    }
}
