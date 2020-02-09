using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Readr.DataObjects
{
    [Table("AppUser")]
    public class AppUser
    {
        [Column("AppUserId")]
        public int AppUserId { get; set; }

        [Column("Username")]
        public string Username { get; set; }
    }
}
