using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models
{
    public interface IAzureSettings
    {
        string ApiKey { get; set; }
        string RootUrl { get; set; }
        string OcpApimSubscriptionKeyHeader { get; set; }
    }
}
