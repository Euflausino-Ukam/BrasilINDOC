using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client
{
    public class ClientParams
    {
        [JsonPropertyName("bankId")]
        public string? BankId { get; set; }

        [JsonPropertyName("agreementId")]
        public string? AgreementId { get; set; }

        [JsonPropertyName("proposalNumber")]
        public string? ProposalNumber { get; set; }

        [JsonPropertyName("proposalValue")]
        public double? ProposalValue { get; set; }

        [JsonPropertyName("partnerCod")]
        public string? PartnerCod { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("clientName")]
        public string? ClientName { get; set; }

        [JsonPropertyName("clientCpf")]
        public string? ClientCpf { get; set; }

        [JsonPropertyName("clientCnpj")]
        public string? ClientCnpj { get; set; }

        [JsonPropertyName("clientBirthDate")]
        public string? ClientBirthDate { get; set; }

        [JsonPropertyName("performanceSquare")]
        public string? PerformanceSquare { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("branch")]
        public string? Branch { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("legalRepresentativeName")]
        public string? LegalRepresentativeName { get; set; }

        [JsonPropertyName("legalRepresentativeNickname")]
        public string? LegalRepresentativeNickname { get; set; }

        [JsonPropertyName("legalRepresentativeDocument")]
        public string? LegalRepresentativeDocument { get; set; }

        [JsonPropertyName("formalization")]
        public int? Formalization { get; set; }

        [JsonPropertyName("documentType")]
        public int? DocumentType { get; set; }

        [JsonPropertyName("expeditionDate")]
        public string? ExpeditionDate { get; set; }

        [JsonPropertyName("hasSelfie")]
        public bool? HasSelfie { get; set; }

        [JsonPropertyName("attachments")]
        public List<AttachmentParams> Attachments { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public ClientParams()
        {
            Attachments = new List<AttachmentParams>();
        }

        public void UpdateAttachments(List<AttachmentParams> attachments)
        {
            this.Attachments ??= new List<AttachmentParams>();

            if (attachments.Any())
            {
                foreach (var attachment in attachments)
                {
                    var existingAttachment = this.Attachments
                        .FirstOrDefault(a => a.AttachType == attachment.AttachType);

                    if (existingAttachment != null)
                    {
                        existingAttachment.FileId = attachment.FileId;
                        existingAttachment.AttachType = attachment.AttachType ?? 4;
                    }
                    else
                    {
                        this.Attachments.Add(new AttachmentParams(attachment));
                    }
                }
            }
        }

        public bool IsValid()
        {
            var erros = new List<string>();

            if (!Type.HasValue)
                erros.Add("O tipo do cliente não é valido.");

            if (string.IsNullOrWhiteSpace(ClientName))
                erros.Add("O nome do cliente não é valido.");

            if (!IdentityValidate.ValidateCpf(ClientCpf) && !IdentityValidate.ValidateCnpj(ClientCnpj))
                erros.Add("A identificação do cliente não é valida.");

            if (!DateValidate.ValidateBithDate(ClientBirthDate) && (Type == 1 || Type == 3))
                erros.Add("A data de nascimento do cliente não é valida.");

            if (!Formalization.HasValue)
                erros.Add("A formalização do cliente não é valida.");

            if (!DateValidate.ValidateDocumentExpeditionDate(ExpeditionDate))
                erros.Add("A data de expedição do documento do cliente não é valida.");

            if (string.IsNullOrWhiteSpace(Message))
                erros.Add("A mensagem do cliente não é valida.");

            if (erros.Count > 0)
                throw new ErrorLists(erros);

            return true;
        }
    }
}
