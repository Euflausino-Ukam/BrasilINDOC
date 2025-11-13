using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels;
using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client
{
    public class ClientResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("cod")]
        public int? Cod { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("origin")]
        public int? Origin { get; set; }

        [JsonPropertyName("documentType")]
        public int? DocumentType { get; set; }

        [JsonPropertyName("bank")]
        public Bank Bank { get; set; }

        [JsonPropertyName("agreement")]
        public Agreement Agreement { get; set; }

        [JsonPropertyName("partnerCod")]
        public string PartnerCod { get; set; }

        [JsonPropertyName("clientName")]
        public string ClientName { get; set; }

        [JsonPropertyName("clientCpf")]
        public string ClientCpf { get; set; }

        [JsonPropertyName("clientCnpj")]
        public string ClientCnpj { get; set; }

        [JsonPropertyName("clientBirthDate")]
        public string ClientBirthDate { get; set; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("finishedAt")]
        public string FinishedAt { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("requester")]
        public Requester Requester { get; set; }

        [JsonPropertyName("automationIsDone")]
        public bool? AutomationIsDone { get; set; }

        [JsonPropertyName("originId")]
        public string OriginId { get; set; }

        [JsonPropertyName("proposalNumber")]
        public string ProposalNumber { get; set; }

        [JsonPropertyName("proposalValue")]
        public string ProposalValue { get; set; }

        [JsonPropertyName("legalRepresentativeName")]
        public string LegalRepresentativeName { get; set; }

        [JsonPropertyName("legalRepresentativeNickname")]
        public string LegalRepresentativeNickname { get; set; }

        [JsonPropertyName("legalRepresentativeDocument")]
        public string LegalRepresentativeDocument { get; set; }

        [JsonPropertyName("hasSelfie")]
        public bool? HasSelfie { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("branch")]
        public string Branch { get; set; }

        [JsonPropertyName("formalization")]
        public int? Formalization { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("expeditionDate")]
        public string ExpeditionDate { get; set; }

        [JsonPropertyName("inBlocklist")]
        public bool? InBlocklist { get; set; }

        [JsonPropertyName("obsBlocklist")]
        public string ObsBlocklist { get; set; }

        [JsonPropertyName("flags")]
        public List<Flag> Flags { get; set; }

        [JsonPropertyName("attachments")]
        public List<AttachmentParams> Attachments { get; set; }

        [JsonPropertyName("interactions")]
        public List<Interaction> Interactions { get; set; }

        [JsonPropertyName("internalAnnotations")]
        public List<InternalAnnotation> InternalAnnotations { get; set; }

        [JsonPropertyName("automations")]
        public Automations Automations { get; set; }

        [JsonPropertyName("interactionHistory")]
        public InteractionHistory InteractionHistory { get; set; }

        [JsonConstructor]
        public ClientResult() { }
    }
}
