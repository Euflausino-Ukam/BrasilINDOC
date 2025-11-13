using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class Receiver
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("profile")]
        public int? Profile { get; set; }

        [JsonPropertyName("userMaster")]
        public UserMaster UserMaster { get; set; }

        [JsonPropertyName("company")]
        public Company Company { get; set; }

        [JsonPropertyName("person")]
        public Person Person { get; set; }
    }
}
