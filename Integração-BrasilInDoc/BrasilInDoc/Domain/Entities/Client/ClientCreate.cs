using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Domain;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;

namespace Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client
{
    public partial class ClientApplication
    {
        public ClientApplication CreateClientCreate(ClientParams @params)
        {
            if (@params == null) 
                throw new ErrorLists(["Os paramentros do cliente são nulos"]);

            this.Id = Guid.NewGuid();
            this.BankId = @params.BankId;
            this.AgreementId = @params.AgreementId;
            this.ProposalNumber = @params.ProposalNumber;
            this.ProposalValue = @params.ProposalValue;
            this.PartnerCod = @params.PartnerCod;
            this.Type = (ApplicationType?)@params.Type;
            this.ClientName = @params.ClientName;  
            this.ClientCpf = @params.ClientCpf;
            this.ClientCnpj = @params.ClientCnpj;
            this.ClientBirthDate = @params.ClientBirthDate;
            this.PerformanceSquare = @params.PerformanceSquare;
            this.Phone = @params.Phone;
            this.Branch = @params.Branch;
            this.State = @params.State;
            this.LegalRepresentativeName = @params.LegalRepresentativeName;
            this.LegalRepresentativeNickname = @params.LegalRepresentativeNickname;
            this.LegalRepresentativeDocument = @params.LegalRepresentativeDocument;
            this.Formalization = (FormalizationType?)@params.Formalization;
            this.DocumentType = (DocumentType?)@params.DocumentType;
            this.ExpeditionDate = @params.ExpeditionDate;
            this.HasSelfie = @params.HasSelfie;
            this.Message = @params.Message;

            if (@params.Attachments.Any())
                this.Attachments = [.. @params.Attachments.Select(a => new AttachmentTypes(a))];
            else
                this.Attachments = [];

            this.CreatedAt = DateTime.UtcNow;

            return this;
        }
    }
}
