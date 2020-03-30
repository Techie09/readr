using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Readr.Services.Interfaces
{
    public interface IAzureService
    {
        public Task AddScan(string userSessionId, byte[] imageData);
        public HttpRequestHeaders AddSubscriptionHeader(HttpRequestHeaders headers);
    }
}
