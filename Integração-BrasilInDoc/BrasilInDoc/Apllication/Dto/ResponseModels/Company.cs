using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class Company
    {
        [JsonPropertyName("fantasyName")]
        public string FantasyName { get; set; }

        [JsonPropertyName("corporateName")]
        public string CorporateName { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }
    }
}
