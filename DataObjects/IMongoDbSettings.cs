namespace Readr.Models
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
