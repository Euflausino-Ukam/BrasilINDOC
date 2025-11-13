using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class InternalAnnotation
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("createdBy")]
        public CreatedBy CreatedBy { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
    }
}
