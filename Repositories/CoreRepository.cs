using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Readr.Models;
using System.Reactive;
using System.Reactive.Linq;

namespace Readr.Repositories
{
    public class CoreRepository<T> where T : IBsonModel
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<T> _collection;

        public string CollectionName { get; private set; }

        public CoreRepository(IMongoDbSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _db = _client.GetDatabase(settings.DatabaseName);
        }

        public async Task Init(string collectionName)
        {
            await new Task(() => 
            { 
                CollectionName = collectionName; 
                _collection = _db.GetCollection<T>(CollectionName); 
            });
        }

        public async IAsyncEnumerable<T> GetAll()
        {
            using (IAsyncCursor<T> cursor = await _collection.FindAsync(new BsonDocument()))
            {
                IEnumerable<T> batch = cursor.Current;
                foreach (T document in batch)
                {
                    yield return document;
                }
            }
        }

        public async Task<T> GetById(string id)
        {
            return await _collection.Find(u => u.Id == ObjectId.Parse(id)).SingleAsync();
        }

        public async Task Add(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task AddMany(IEnumerable<T> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        //public async Task Update(string id, T document)
        //{
        //    //TODO: finish writing this function
        //    var filter = new BsonDocument("id", id);
        //    var update = new BsonDocument("set", document);
        //    var options = new UpdateOptions { IsUpsert = true };
        //    await _collection.UpdateManyAsync(filter, )
        //}
    }
}
