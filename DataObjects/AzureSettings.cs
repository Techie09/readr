using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models
{
    public class AzureSettings : IAzureSettings
    {
        public string ApiKey { get; set; }
        public string RootUrl { get; set; }
        public string OcpApimSubscriptionKeyHeader { get; set; }
    }
}
