using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readr.Models;
using Readr.Services.Interfaces;

namespace Readr.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/api/AppUsers")]
    public class AppUserController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        /// <summary>
        /// This APi endpoint uses <see cref="HttpPutAttribute"/> and attempts to add a new user to the system
        /// This will fail if the username already exists in the system.
        /// </summary>
        /// <example>
        /// Make an Http request to "http://localhost:5000/api/AppUsers/add/test" where test is the username of the end-user 
        /// </example>
        /// <exception cref="ApplicationException">Thrown when the <paramref name="username"/> already exists in the datastore</exception>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPut("add/{username}")]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("login/{username}")]
        public async Task<IActionResult> LoginAppUserAsync(string username)
        {
            //TryCatch<string, AppUser>(async () = await _appUserService.LoginAppUserAsync(username), username);

            try
            {
                //verify the AppUser exists
                var appUser = await _appUserService.LoginAppUserAsync(username);
                return Ok(appUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public IActionResult TryCatch<T, TResult>(Func<T, TResult>  func, T param)
        {
            try
            {
                TResult result = func.Invoke(param);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
