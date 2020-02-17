using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readr.Assets.Scripts.Models
{
    public class UserSession
    {
        public string Id { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
