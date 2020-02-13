using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Readr.Models;

namespace Services
{
    public interface IAppUserService
    {
        Task<AppUser> AddAppUserAsync(string username);
    }
}
