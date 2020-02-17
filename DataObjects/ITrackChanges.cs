using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Readr.Models
{
    public interface ITrackChanges
    {
        /// <summary>
        /// this should be the Id of the User
        /// </summary>
        public ObjectId ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public static partial class ExtensionMethods
    {
        public static async Task SetModifiedPropertiesAsync(this ITrackChanges trackChanges, ObjectId modifiedById, DateTime modifiedOn)
        {
            await Task.Run(() => 
            { 
                trackChanges.ModifiedById = modifiedById; 
                trackChanges.ModifiedOn = modifiedOn; 
            });
        }
    }
}
