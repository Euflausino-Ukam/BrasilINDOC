using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels
{
    public class Person
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sectorExpertise")]
        public SectorExpertise SectorExpertise { get; set; }

        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }
    }
}
