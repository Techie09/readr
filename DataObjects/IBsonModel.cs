using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Readr.Models
{
    public interface IBsonModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
