using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Webhooks;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal
{
    public class WebhookRepository : IWebhookRepository
    {
        private readonly IMongoCollection<Webhook> _collection;

        public WebhookRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("BrasilInDoc-Local");

            _collection = database.GetCollection<Webhook>("Webhook");
        }

        public async Task SaveWebhookAsync(Webhook webhook)
        {
            await _collection.InsertOneAsync(webhook);
        }

        public async Task<Webhook> GetWebhookAsync()
        {
            var filter = Builders<Webhook>.Filter.Eq(w => w.Flag, false);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateWebhookAsync(Webhook webhook)
        {
            var filter = Builders<Webhook>.Filter.Eq(w => w.Id, webhook.Id);
            await _collection.ReplaceOneAsync(filter, webhook, new ReplaceOptions { IsUpsert = true });
        }
    }
}
