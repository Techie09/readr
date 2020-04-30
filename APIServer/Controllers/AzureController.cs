using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readr.Services.Interfaces;

namespace Readr.Api.Controllers
{
    [Route("api/azure")]
    public class AzureController : Controller
    {
        private readonly IAzureService _azureService;

        public AzureController(IAzureService azureService)
        {
            _azureService = azureService;
        }

        [HttpPost("addscan/{userSessionId}")]
        public async Task<IActionResult> AddScanToSesionAsync(string userSessionId)
        {
            try
            {
                var imageData = await Request.GetRawBodyBytesAsync();

                await _azureService.AddScan(userSessionId, imageData);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
