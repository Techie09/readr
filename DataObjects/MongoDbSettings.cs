namespace Readr.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
    }
}