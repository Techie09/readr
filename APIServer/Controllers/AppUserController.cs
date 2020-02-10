using System;
using System.Collections.Generic;
using System.Text;
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

        [HttpPost("/add/{username}")]
        public IActionResult AddAppUser(string username)
        {
            try
            {
                var appUser = _appUserService.AddAppUser(username);
                return Ok(appUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("/login/{username}")]
        public IActionResult LoginAppUser(string username)
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
