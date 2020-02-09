using System;
using System.Collections.Generic;
using System.Text;
using Readr.DataObjects;

namespace Services
{
    public interface IAppUserService
    {
        AppUser AddAppUser(string username);
    }
}
