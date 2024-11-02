using Domain.Entidades;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Domain.Servicos
{
    public class LogMongoService
    {
        private readonly IMongoCollection<LogMongo> _logsCollection;

        public LogMongoService(IOptions<MongoDBSettings> mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
            _logsCollection = database.GetCollection<LogMongo>(mongoSettings.Value.LogsCollectionName);
        }

        public Task LogAsync(LogMongo log)
        {
           var logMongo = _logsCollection.InsertOneAsync(log);
            return logMongo;
        }
    }
}
