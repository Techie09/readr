using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models
{
    public class UserSessionDto
    {
        public string Id { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
