namespace Readr.Models
{
    public interface IMongoDbSettings
    {
        string ConnectionStrings { get; set; }
        string DatabaseName { get; set; }
    }
}
