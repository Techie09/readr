using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readr.Models;
using Readr.Services.Interfaces;

namespace Readr.Api.Controllers
{
    [Route("api/Sessions")]
    public class UserSessionController : Controller
    {
        private readonly IUserSessionService _userSessionService;

        public UserSessionController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpPost("upsert/")]
        public async Task<IActionResult> UpsertUserSessionAsync([FromBody]UserSessionDto userSessionDto)
        {
            try
            {
                var userSession = UserSession.CreateUserSession(userSessionDto.Isbn, userSessionDto.Description, userSessionDto.ModifiedById);
                var userSessionResult = await _userSessionService.UpsertUserSessionAsync(userSession).ConfigureAwait(false);
                return Ok(userSessionResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("id/{sessionId}/search/{searchText}/")]
        public async Task<IActionResult> SearchSessionForText(string sessionId, string searchText)
        {
            try
            {
                var searchSessionResult = await _userSessionService.SearchUserSession(sessionId, searchText).ConfigureAwait(false);
                return Ok(searchSessionResult);
            }
            catch ( Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
