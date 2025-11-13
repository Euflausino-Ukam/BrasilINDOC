using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class Interaction
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("receiver")]
        public Receiver Receiver { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("sendedAt")]
        public string SendedAt { get; set; }
    }
}
