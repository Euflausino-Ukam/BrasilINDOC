using Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client;
using MongoDB.Driver;
using System.Text.Json;

namespace Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly IMongoCollection<ClientApplication> _collection;

        public AnalysisRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("BrasilInDoc-Local");

            _collection = database.GetCollection<ClientApplication>("Client");
        }

        public async Task CreateAnalysisAsync(ClientApplication clientResult)
        {
            await _collection.InsertOneAsync(clientResult);
        }

        public async Task<ClientApplication?> GetClientAnalysisAsync(string externalId)
        {
            var filter = Builders<ClientApplication>.Filter.Eq(c => c.ExternalId, externalId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ClientApplication?> GetClientByIdentityAnalysisAsync(string identity)
        {
            var cpfFilter = Builders<ClientApplication>.Filter.Eq(c => c.ClientCpf, identity);
            var cnpjFilter = Builders<ClientApplication>.Filter.Eq(c => c.ClientCnpj, identity);

            var filter = Builders<ClientApplication>.Filter.Or(cpfFilter, cnpjFilter);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
            
        public async Task UpdateClientAnalysisAsync(ClientApplication clientResult)
        {
            var filter = Builders<ClientApplication>.Filter.Eq(c => c.Id, clientResult.Id);
            await _collection.ReplaceOneAsync(filter, clientResult, new ReplaceOptions { IsUpsert = true });
        }
    }
}
