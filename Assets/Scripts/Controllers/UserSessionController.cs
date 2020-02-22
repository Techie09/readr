using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readr.Assets.Scripts.Models;

namespace Readr.Assets.Scripts.Controllers
{
    public class UserSessionController : WebRequestController
    {
        public UserSessionController() : base("Session")
        {

        }

        public async Task<UserSession> CreateSessionAsync(string appUserId, string isbn, string description)
        {
            var response = await Post($"/create/{appUserId}/{isbn}/{description}");
            return await response.GetData<UserSession>();
        }
    }
}
