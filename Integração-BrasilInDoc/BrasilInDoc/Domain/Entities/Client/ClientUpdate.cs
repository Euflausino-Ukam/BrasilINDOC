using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Domain;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;

namespace Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client
{
    public partial class ClientApplication
    {
        // Atualização conforme os paramentros
        public ClientApplication UpdateClient(ClientParams @params)
        {
            if (@params == null)
                throw new ErrorLists(["Parâmetros inválidos para atualização do cliente."]);

            if(!string.IsNullOrEmpty(@params.BankId))
                this.BankId = @params.BankId;

            if (!string.IsNullOrEmpty(@params.AgreementId))
                this.AgreementId = @params.AgreementId;

            if (!string.IsNullOrEmpty(@params.ProposalNumber))
                this.ProposalNumber = @params.ProposalNumber;

            if (@params.ProposalValue.HasValue)
                this.ProposalValue = @params.ProposalValue;

            if (!string.IsNullOrEmpty(@params.PartnerCod))
                this.PartnerCod = @params.PartnerCod;

            if (@params.Type.HasValue)
            {
                if (this.Type != null && this.Type != (ApplicationType)@params.Type.Value)
                    throw new ErrorLists(["Não é possível alterar o tipo de cliente."]);
                this.Type = (ApplicationType)@params.Type.Value;
            }

            if (!string.IsNullOrEmpty(@params.ClientName))
                this.ClientName = @params.ClientName;

            if (!string.IsNullOrEmpty(@params.ClientCpf))
                this.ClientCpf = @params.ClientCpf;

            if (!string.IsNullOrEmpty(@params.ClientCnpj))
                this.ClientCnpj = @params.ClientCnpj;

            if (!string.IsNullOrEmpty(@params.ClientBirthDate))
                this.ClientBirthDate = @params.ClientBirthDate;

            if (!string.IsNullOrEmpty(@params.PerformanceSquare))
                this.PerformanceSquare = @params.PerformanceSquare;

            if (!string.IsNullOrEmpty(@params.Phone))
                this.Phone = @params.Phone;

            if (!string.IsNullOrEmpty(@params.Branch))
                this.Branch = @params.Branch;

            if (!string.IsNullOrEmpty(@params.State))
                this.State = @params.State;

            if (!string.IsNullOrEmpty(@params.LegalRepresentativeName))
                this.LegalRepresentativeName = @params.LegalRepresentativeName;

            if (!string.IsNullOrEmpty(@params.LegalRepresentativeNickname))
                this.LegalRepresentativeNickname = @params.LegalRepresentativeNickname;

            if (!string.IsNullOrEmpty(@params.LegalRepresentativeDocument))
                this.LegalRepresentativeDocument = @params.LegalRepresentativeDocument;

            if (@params.Formalization.HasValue)
                this.Formalization = (FormalizationType)@params.Formalization.Value;

            if (@params.DocumentType.HasValue)
                this.DocumentType = (DocumentType)@params.DocumentType.Value;

            if (!string.IsNullOrEmpty(@params.ExpeditionDate))
                this.ExpeditionDate = @params.ExpeditionDate;

            if (@params.HasSelfie.HasValue)
                this.HasSelfie = @params.HasSelfie;

            if (!string.IsNullOrEmpty(@params.Message))
                this.Message = @params.Message;

            if (@params.Attachments != null && @params.Attachments.Any())
            {
                this.Attachments ??= [];

                foreach(var attachment in @params.Attachments)
                {
                    var existingAttachment = this.Attachments
                        .FirstOrDefault(a => a.AttachType == attachment.AttachType);

                    if (existingAttachment != null)
                    {
                        existingAttachment.Id = attachment.FileId;
                        existingAttachment.Name = existingAttachment.TypeToString(attachment.AttachType ?? 4);
                    }
                    else
                    {
                        this.Attachments.Add(new AttachmentTypes(attachment));
                    }
                }
            }

            this.UpdatedAt = DateTime.UtcNow;

            return this;
        }

        // Atualização conforme o result
        public ClientApplication UpdateClient(ClientResult clientResult)
        {
            if (clientResult == null)
                throw new ErrorLists(["Resultado do cliente inválido para atualização."]);

            if (!string.IsNullOrEmpty(clientResult.Id))
                this.ExternalId = clientResult.Id;

            if (clientResult.Cod.HasValue)
                this.Cod = clientResult.Cod;

            if (clientResult.Status.HasValue)
                this.Status = (StatusType)clientResult.Status;

            if (clientResult.Origin.HasValue)
                this.Origin = clientResult.Origin;

            if (clientResult.DocumentType.HasValue)
                this.DocumentType = (DocumentType)clientResult.DocumentType;

            if (clientResult.Bank is not null)
                this.Bank = clientResult.Bank!;

            if (clientResult.Agreement is not null)
                this.Agreement = clientResult.Agreement;

            if (!string.IsNullOrWhiteSpace(clientResult.PartnerCod))
                this.PartnerCod = clientResult.PartnerCod;

            if (!string.IsNullOrWhiteSpace(clientResult.ClientName))
                this.ClientName = clientResult.ClientName;

            if (!string.IsNullOrWhiteSpace(clientResult.ClientCpf))
                this.ClientCpf = clientResult.ClientCpf;

            if (!string.IsNullOrWhiteSpace(clientResult.ClientCnpj))
                this.ClientCnpj = clientResult.ClientCnpj;

            if (!string.IsNullOrWhiteSpace(clientResult.ClientBirthDate))
                this.ClientBirthDate = clientResult.ClientBirthDate;

            if (!string.IsNullOrWhiteSpace(clientResult.CreatedAt))
                this.ExternalCreatedAt = clientResult.CreatedAt;

            if (!string.IsNullOrWhiteSpace(clientResult.UpdatedAt))
                this.ExternalUpdatedAt = clientResult.UpdatedAt;

            if (!string.IsNullOrWhiteSpace(clientResult.FinishedAt))
                this.ExternalFinishedAt = clientResult.FinishedAt;

            if (!string.IsNullOrWhiteSpace(clientResult.Operator))
                this.Operator = clientResult.Operator;

            if (clientResult.Requester is not null)
                this.Requester = clientResult.Requester;

            if (clientResult.AutomationIsDone.HasValue)
                this.AutomationIsDone = clientResult.AutomationIsDone.Value;

            if (!string.IsNullOrWhiteSpace(clientResult.OriginId))
                this.OriginId = clientResult.OriginId;

            if (!string.IsNullOrWhiteSpace(clientResult.ProposalNumber))
                this.ProposalNumber = clientResult.ProposalNumber;

            if (!string.IsNullOrWhiteSpace(clientResult.ProposalValue))
                this.ProposalValue = Double.Parse(clientResult.ProposalValue);

            if (!string.IsNullOrWhiteSpace(clientResult.LegalRepresentativeName))
                this.LegalRepresentativeName = clientResult.LegalRepresentativeName;

            if (!string.IsNullOrWhiteSpace(clientResult.LegalRepresentativeNickname))
                this.LegalRepresentativeNickname = clientResult.LegalRepresentativeNickname;

            if (!string.IsNullOrWhiteSpace(clientResult.LegalRepresentativeDocument))
                this.LegalRepresentativeDocument = clientResult.LegalRepresentativeDocument;

            if (clientResult.HasSelfie.HasValue)
                this.HasSelfie = clientResult.HasSelfie.Value;

            if (!string.IsNullOrWhiteSpace(clientResult.Phone))
                this.Phone = clientResult.Phone;

            if (!string.IsNullOrWhiteSpace(clientResult.Branch))
                this.Branch = clientResult.Branch;

            if (clientResult.Formalization.HasValue)
                this.Formalization = (FormalizationType)clientResult.Formalization;

            if (!string.IsNullOrWhiteSpace(clientResult.State))
                this.State = clientResult.State;

            if (!string.IsNullOrWhiteSpace(clientResult.ExpeditionDate))
                this.ExpeditionDate = clientResult.ExpeditionDate;

            if (clientResult.InBlocklist.HasValue)
                this.InBlocklist = clientResult.InBlocklist.Value;

            if (!string.IsNullOrWhiteSpace(clientResult.ObsBlocklist))
                this.ObsBlocklist = clientResult.ObsBlocklist;

            if (clientResult.Flags != null && clientResult.Flags.Any())
            {
                this.Flags ??= [];
                this.Flags.Clear();
                foreach (var f in clientResult.Flags)
                {
                    this.Flags.Add(f);
                }
            }

            if (clientResult.Interactions != null && clientResult.Interactions.Count != 0)
            {
                this.Interactions ??= [];
                this.Interactions.Clear();
                foreach (var inter in clientResult.Interactions)
                    this.Interactions.Add(inter);
            }

            if (clientResult.InternalAnnotations != null && clientResult.InternalAnnotations.Count != 0)
            {
                this.InternalAnnotations ??= [];
                this.InternalAnnotations.Clear();
                foreach (var ia in clientResult.InternalAnnotations)
                    this.InternalAnnotations.Add(ia);
            }

            if (clientResult.Attachments != null && clientResult.Attachments.Count != 0)
            {
                this.Attachments ??= [];

                foreach (var attachment in clientResult.Attachments)
                {
                    var existingAttachment = this.Attachments
                        .FirstOrDefault(a => a.AttachType == attachment.AttachType);

                    if (existingAttachment != null)
                    {
                        existingAttachment.Id = attachment.FileId;
                        existingAttachment.Name = existingAttachment.TypeToString(attachment.AttachType ?? 4);
                    }
                    else
                    {
                        this.Attachments.Add(new AttachmentTypes(attachment));
                    }
                }
            }

            if (clientResult.Automations is not null)
                this.Automations = clientResult.Automations;

            if (clientResult.InteractionHistory is not null)
                this.InteractionHistory = clientResult.InteractionHistory;

            if (string.IsNullOrWhiteSpace(clientResult.UpdatedAt))
                this.ExternalUpdatedAt = clientResult.UpdatedAt;

            this.UpdatedAt = DateTime.UtcNow;

            return this;
        }
    }
}
