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
        private IAppUserService _appUserService;

        public AppUserController()
        {

        }

        [HttpPost("add/{userName}")]
        public IActionResult AddAppUser(string userName)
        {
            //Add AppUser to Db
            //verify the username does not already exists
            return Ok();

            //if the usename already exists, return error message
        }

        [HttpPost("login/{username}")]
        public IActionResult LoginAppUser(string username)
        {
            //verify the AppUser exists
            return Ok();

            //If the user does not exists, throwe error message
        }
    }
}
