using System;
using System.Collections.Generic;
using System.Text;

namespace Readr.Models.models
{
    public interface ITrackChanges
    {
        /// <summary>
        /// this should be the Id of the User
        /// </summary>
        public string Id { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
