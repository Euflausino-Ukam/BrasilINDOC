using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class Automations
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("federalRecipe")]
        public int? FederalRecipe { get; set; }

        [JsonPropertyName("contactInfo")]
        public int? ContactInfo { get; set; }
    }
}
