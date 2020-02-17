using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readr.Assets.Scripts.Models
{
    [Serializable]
    public class AppUser
    {
        public string id { get; set; }
        public string username { get; set; }

        public AppUser()
        {

        }
    }
}
