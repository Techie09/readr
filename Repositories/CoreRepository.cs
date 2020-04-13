using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Readr.Models;

namespace Readr.Repositories
{
    public class CoreRepository<T>: IDisposable where T : IBsonModel
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<T> _collection;

        public string CollectionName { get; private set; }

        public CoreRepository(IMongoDbSettings settings)
        {
            _client = new MongoClient(settings.ConnectionStrings);
            _db = _client.GetDatabase(settings.DatabaseName);
        }

        public void Init(string collectionName)
        {
            CollectionName = collectionName; 
            var dbCollection = _db.GetCollection<T>(CollectionName);
            
            if (dbCollection == null)
            {
                _db.CreateCollection(CollectionName);
                dbCollection = _db.GetCollection<T>(CollectionName);
            }
            _collection = dbCollection;
        }

        protected async IAsyncEnumerable<T> Get()
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

        protected async IAsyncEnumerable<T> Get(Expression<Func<T,bool>> filter)
        {
            using (IAsyncCursor<T> cursor = await _collection.FindAsync(filter))
            {
                IEnumerable<T> batch = cursor.Current;
                foreach (T document in batch)
                {
                    yield return document;
                }
            }
        }

        public async Task<T> GetById(ObjectId id)
        {
            return await _collection.Find(u => u.Id == id).SingleAsync();
        }

        public async Task<T> GetBy(Expression<Func<T,bool>> filter)
        {
            return await _collection.Find(filter).SingleAsync();
        }

        protected async Task InsertAsync(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        protected async Task InsertAsync(T document, InsertOneOptions options)
        {
            await _collection.InsertOneAsync(document, options);
        }

        protected async Task InsertAsync(IEnumerable<T> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        protected async Task InsertAsync(IEnumerable<T> documents, InsertManyOptions options)
        {
            await _collection.InsertManyAsync(documents);
        }

        protected async Task FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition, FindOneAndUpdateOptions<T,T> options)
        {
            await _collection.FindOneAndUpdateAsync(filter, definition, options);
        }

        protected async Task FindOneandUpdateAsync(Expression<Func<T,bool>> filter, UpdateDefinition<T> definition, FindOneAndUpdateOptions<T> options)
        {
            await _collection.FindOneAndUpdateAsync(filter, definition, options);
        }

        protected async Task UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> definition, UpdateOptions options)
        {
            await _collection.UpdateOneAsync(filter, definition, options);
        }

        protected async Task UpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> definition, UpdateOptions options)
        {
            await _collection.UpdateOneAsync(filter, definition, options);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _client = null;
                    _db = null;
                    _collection = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CoreRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
