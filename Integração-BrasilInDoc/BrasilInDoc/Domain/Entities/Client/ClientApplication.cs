using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels;
using Integração_BrasilInDoc.BrasilInDoc.Domain;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client
{
    public partial class ClientApplication
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid? Id { get; set; }

        [BsonElement("type")]
        public ApplicationType? Type { get; set; }

        [BsonElement("externalId")]
        public string? ExternalId { get; set; }

        [BsonElement("clientName")]
        public string? ClientName { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("expeditionDate")]
        public string? ExpeditionDate { get; set; }

        [BsonElement("state")]
        public string? State { get; set; }

        [BsonElement("hasSelfie")]
        public bool? HasSelfie { get; set; }

        [BsonElement("message")]
        public string? Message { get; set; }

        [BsonElement("bankId")]
        public string? BankId { get; set; }

        [BsonElement("agreementId")]
        public string? AgreementId { get; set; }

        [BsonElement("proposalNumber")]
        public string? ProposalNumber { get; set; }

        [BsonElement("proposalValue")]
        public double? ProposalValue { get; set; }

        [BsonElement("partnerCod")]
        public string? PartnerCod { get; set; }

        [BsonElement("performanceSquare")]
        public string? PerformanceSquare { get; set; }

        [BsonElement("clientCpf")]
        public string? ClientCpf { get; set; }

        [BsonElement("clientCnpj")]
        public string? ClientCnpj { get; set; }

        [BsonElement("clientBirthDate")]
        public string? ClientBirthDate { get; set; }

        [BsonElement("branch")]
        public string? Branch { get; set; }

        [BsonElement("legalRepresentativeName")]
        public string? LegalRepresentativeName { get; set; }

        [BsonElement("legalRepresentativeNickname")]
        public string? LegalRepresentativeNickname { get; set; }

        [BsonElement("legalRepresentativeDocument")]
        public string? LegalRepresentativeDocument { get; set; }

        [BsonElement("cod")]
        public int? Cod { get; set; }

        [BsonElement("status")]
        public StatusType? Status { get; set; }

        [BsonElement("origin")]
        public int? Origin { get; set; }

        [BsonElement("externalCreatedAt")]
        public string? ExternalCreatedAt { get; set; }

        [BsonElement("externalUpdatedAt")]
        public string? ExternalUpdatedAt { get; set; }

        [BsonElement("externalFinishedAt")]
        public string? ExternalFinishedAt { get; set; }

        [BsonElement("operator")]
        public string? Operator { get; set; }

        [BsonElement("automationIsDone")]
        public bool? AutomationIsDone { get; set; }

        [BsonElement("originId")]
        public string? OriginId { get; set; }

        [BsonElement("inBlocklist")]
        public bool? InBlocklist { get; set; }

        [BsonElement("obsBlocklist")]
        public string? ObsBlocklist { get; set; }

        [BsonElement("attachments")]
        public IList<AttachmentTypes>? Attachments { get; set; }

        [BsonElement("formalization")]
        public FormalizationType? Formalization { get; set; }

        [BsonElement("documentType")]
        public DocumentType? DocumentType { get; set; }

        [BsonElement("bank")]
        public Bank Bank { get; set; }

        [BsonElement("agreement")]
        public Agreement Agreement { get; set; }

        [BsonElement("requester")]
        public Requester Requester { get; set; }

        [BsonElement("flags")]
        public List<Flag> Flags { get; set; }

        [BsonElement("interactions")]
        public List<Interaction> Interactions { get; set; }

        [BsonElement("internalAnnotations")]
        public List<InternalAnnotation> InternalAnnotations { get; set; }

        [BsonElement("automations")]
        public Automations Automations { get; set; }

        [BsonElement("interactionHistory")]
        public InteractionHistory InteractionHistory { get; set; }


        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }


        [BsonElement("requestPayload")]
        public List<string> RequestPayload { get; set; }

        [BsonElement("responsePayload")]
        public List<string> ResponsePayload { get; set; }


        public ClientApplication()
        {
            Id = Guid.NewGuid();
            Attachments = [];
            Flags = [];
            Interactions = [];
            InternalAnnotations = [];
            Automations = new Automations();
            InteractionHistory = new InteractionHistory();
            RequestPayload = [];
            ResponsePayload = [];
        }
    }
}
