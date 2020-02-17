using System.Threading.Tasks;
using Readr.Models;

namespace Readr.Services.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUserDto> AddAppUserAsync(string username);
        Task<AppUserDto> LoginAppUserAsync(string username);
    }
}
