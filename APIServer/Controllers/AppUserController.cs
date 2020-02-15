using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Readr.Api.Controllers
{
    [Route("AppUser")]
    public class AppUserController: Controller
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost("add/{username}")]
        public async Task<IActionResult> AddAppUserAsync(string username)
        {
            try
            {
                var appUser = await _appUserService.AddAppUserAsync(username).ConfigureAwait(false);
                return Ok(appUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("login/{username}")]
        public async Task<IActionResult> LoginAppUserAsync(string username)
        {
            //verify the AppUser exists
            try
            {
                //var appUser = _appUserService.LoginAppUser(username);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            //If the user does not exists, throwe error message
        }
    }
}
