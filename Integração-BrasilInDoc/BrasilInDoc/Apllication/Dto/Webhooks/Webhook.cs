using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Webhooks
{
    public class Webhook
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("eventType")]
        [JsonPropertyName("eventType")]
        public string EventType { get; set; }

        [BsonElement("data")]
        [JsonPropertyName("data")]
        public WebhookData Data { get; set; }

        [BsonElement("payload")]
        [JsonPropertyName("payload")]
        public string Payload { get; set; }

        [BsonElement("receiverDate")]
        [JsonPropertyName("receiverDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ReceiverDate { get; set; }

        [BsonElement("flag")]
        [JsonPropertyName("flag")]
        public bool Flag { get; set; }

        public Webhook()
        {
            Id = Guid.NewGuid();
            ReceiverDate = DateTime.UtcNow;
            Flag = false;
            Data = new WebhookData();
        }
    }

    [BsonIgnoreExtraElements]
    public class WebhookData
    {
        [BsonElement("id")]
        [JsonPropertyName("id")]
        public string ExternalId { get; set; } = string.Empty;

        [BsonElement("status")]
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}