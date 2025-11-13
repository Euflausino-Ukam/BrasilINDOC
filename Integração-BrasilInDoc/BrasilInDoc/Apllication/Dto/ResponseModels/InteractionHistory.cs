using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class InteractionHistory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("event")]
        public int? Event { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
    }
}
