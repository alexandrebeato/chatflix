using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infra.Data
{
    public class Repository<T> : IRepository<T> where T : Entity<T>
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoClient _mongoClient;
        protected readonly IMongoCollection<T> _mongoCollection;

        public Repository(IConfiguration configuration, IMongoClient mongoClient)
        {
            _configuration = configuration;
            _mongoClient = mongoClient;
            _mongoCollection = _mongoClient.GetDatabase(_configuration["mongoConnection:database"]).GetCollection<T>(typeof(T).Name);
        }

        public Task Delete(Guid id) =>
            _mongoCollection.FindOneAndDeleteAsync(p => p.Id == id);

        public Task<List<T>> GetAll() =>
            _mongoCollection.Find(e => true).ToListAsync();

        public Task<T> GetById(Guid id) =>
            _mongoCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public Task Insert(T entity) =>
            _mongoCollection.InsertOneAsync(entity);

        public Task Update(T entity) =>
            _mongoCollection.FindOneAndReplaceAsync(p => p.Id == entity.Id, entity);
    }
}