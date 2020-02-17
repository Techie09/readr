using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readr.Services.Interfaces;

namespace Readr.Api.Controllers
{
    [Route("Session")]
    public class UserSessionController : Controller
    {
        private readonly IUserSessionService _userSessionService;

        public UserSessionController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpPost("create/{appUserId}/{isbn}/{description}")]
        [HttpPost("update/{appUserId}/{isbn}/{description}")]
        public async Task<IActionResult> UpsertUserSessionAsync(string appUserId, string isbn, string description)
        {
            try
            {
                var userSession = await _userSessionService.CreateUserSessionAsync(appUserId, isbn, description).ConfigureAwait(false);
                return Ok(userSession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
