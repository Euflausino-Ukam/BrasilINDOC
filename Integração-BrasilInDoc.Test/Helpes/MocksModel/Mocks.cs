using Bogus;
using Bogus.Extensions.Brazil;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.ResponseModels;
using Integração_BrasilInDoc.BrasilInDoc.Domain;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client;

namespace Integração_BrasilInDoc.Test.Helpes.MocksModel
{
    public class Mocks
    {
        private readonly Faker _faker = new("pt_BR");

        public ClientParams CreateClientParamsValid()
        {
            return new ClientParams
            {
                BankId = Guid.NewGuid().ToString(),
                AgreementId = Guid.NewGuid().ToString(),
                ProposalNumber = _faker.Random.AlphaNumeric(6),
                ProposalValue = (double?)_faker.Finance.Amount(5000, 40000, 2),
                PartnerCod = _faker.Finance.Account(),
                Type = 1,
                ClientName = _faker.Person.FullName,
                ClientCpf = _faker.Person.Cpf(),
                ClientBirthDate = _faker.Person.DateOfBirth.ToString(),
                PerformanceSquare = _faker.Address.Direction(),
                Phone = _faker.Phone.ToString(),
                Branch = _faker.Address.City(),
                State = _faker.Address.StateAbbr(),
                Formalization = 1,
                DocumentType = 2,
                ExpeditionDate = _faker.Date.Past(8).ToString(),
                HasSelfie = false,
                Message = _faker.Lorem.Sentence(6)
            };
        }
        public ClientApplication CreateClientApllicationValid()
        {
            return new ClientApplication
            {
                BankId = Guid.NewGuid().ToString(),
                ExternalId = Guid.NewGuid().ToString(),
                AgreementId = Guid.NewGuid().ToString(),
                ProposalNumber = _faker.Random.AlphaNumeric(6),
                ProposalValue = (double?)_faker.Finance.Amount(5000, 40000, 2),
                PartnerCod = _faker.Finance.Account(),
                Type = ApplicationType.Client,
                ClientName = _faker.Person.FullName,
                ClientCpf = _faker.Person.Cpf(),
                ClientBirthDate = _faker.Person.DateOfBirth.ToString(),
                PerformanceSquare = _faker.Address.Direction(),
                Phone = _faker.Phone.ToString(),
                Branch = _faker.Address.City(),
                State = _faker.Address.StateAbbr(),
                Formalization = FormalizationType.Digital,
                DocumentType = DocumentType.RG,
                ExpeditionDate = _faker.Date.Future().ToString(),
                HasSelfie = false,
                Message = _faker.Lorem.Sentence(6)
            };
        }
        public ClientParams CreateClientParamsInvalid()
        {
            return new ClientParams
            {
                BankId = Guid.NewGuid().ToString(),
                AgreementId = Guid.NewGuid().ToString(),
                ProposalNumber = _faker.Random.AlphaNumeric(6),
                ProposalValue = (double?)_faker.Finance.Amount(5000, 40000, 2),
                PartnerCod = _faker.Finance.Account(),
                Type = 1,
                ClientName = null,
                ClientCpf = null,
                ClientBirthDate = _faker.Person.DateOfBirth.ToString(),
                PerformanceSquare = _faker.Address.Direction(),
                Phone = _faker.Phone.ToString(),
                Branch = _faker.Address.City(),
                State = _faker.Address.StateAbbr(),
                Formalization = 1,
                DocumentType = 2,
                ExpeditionDate = _faker.Date.Future().ToString(),
                HasSelfie = false,
                Message = _faker.Lorem.Sentence(6)
            };
        }

        public List<FilesArchives> CreateAttamentParamsValid()
        {
            var random = new Random();

            return new List<FilesArchives>
                {
                    new("doc_front", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.img")),
                    new("doc_back", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf")),
                    new("selfie", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.PDF")),
                    new("outher", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.img"))
                };
        }
        public List<FilesArchives> CreateAttamentParamsWithoutSelfieValid()
        {
            var random = new Random();

            return new List<FilesArchives>
                {
                    new("doc_front", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.img")),
                    new("doc_back", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf"))
                };
        }
        public List<FilesArchives> CreateAttamentParamsInvalid()
        {
            var random = new Random();

            return new List<FilesArchives>
                {
                    new("docront", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt")),
                    new("Dox", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt")),
                    new("", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.exe")),
                    new("outhers", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.ass"))
                };
        }
        public List<FilesArchives> CreateAttamentParamsWithoutSelfieInvalid()
        {
            var random = new Random();

            return new List<FilesArchives>
                {
                    new("front", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.123")),
                    new("doc", Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.xml"))
                };
        }

        public ClientResult CreateClientResultValid()
        {
            return new ClientResult
            {
                Id = Guid.NewGuid().ToString(),
                Cod = _faker.Random.Int(1000, 9999),
                Status = _faker.Random.Int(0, 10),
                Origin = _faker.Random.Int(1, 3),
                DocumentType = _faker.Random.Int(1, 2),

                Bank = new Bank
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _faker.Company.CompanyName()
                },
                Agreement = new Agreement
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _faker.Company.CompanyName()
                },

                PartnerCod = _faker.Finance.Account(),
                ClientName = _faker.Person.FullName,
                ClientCpf = _faker.Person.Cpf(),
                ClientCnpj = _faker.Company.Cnpj(),
                ClientBirthDate = _faker.Person.DateOfBirth.ToString("yyyy-MM-dd"),
                CreatedAt = _faker.Date.Past().ToString("yyyy-MM-dd HH:mm:ss"),
                UpdatedAt = _faker.Date.Recent().ToString("yyyy-MM-dd HH:mm:ss"),
                FinishedAt = _faker.Date.Future().ToString("yyyy-MM-dd HH:mm:ss"),
                Operator = _faker.Person.FirstName,

                Requester = new Requester
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = _faker.Internet.UserName(),
                    Profile = _faker.Random.Int(1, 5),
                    UserMaster = null,
                    Company = null,
                    Person = null
                },

                AutomationIsDone = _faker.Random.Bool(),
                OriginId = Guid.NewGuid().ToString(),
                ProposalNumber = _faker.Random.AlphaNumeric(6),
                ProposalValue = _faker.Finance.Amount(5000, 40000, 2).ToString("F2"),

                LegalRepresentativeName = _faker.Person.FullName,
                LegalRepresentativeNickname = _faker.Internet.UserName(),
                LegalRepresentativeDocument = _faker.Person.Cpf(),

                HasSelfie = _faker.Random.Bool(),
                Phone = _faker.Phone.PhoneNumber(),
                Branch = _faker.Address.City(),
                Formalization = _faker.Random.Int(1, 2),
                State = _faker.Address.StateAbbr(),
                ExpeditionDate = _faker.Date.Past().ToString("yyyy-MM-dd"),
                InBlocklist = _faker.Random.Bool(),
                ObsBlocklist = _faker.Lorem.Sentence(),

                Flags = new List<Flag>
        {
            new Flag
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Flag Teste",
                CreatedAt = _faker.Date.Past().ToString("yyyy-MM-dd HH:mm:ss"),
                UpdatedAt = _faker.Date.Recent().ToString("yyyy-MM-dd HH:mm:ss")
            }
        },
                Attachments = new List<AttachmentParams>
        {
            new AttachmentParams(Guid.NewGuid().ToString(), 1),
            new AttachmentParams(Guid.NewGuid().ToString(), 3)
        },

                Interactions = new List<Interaction>
        {
            new Interaction
            {
                Id = Guid.NewGuid().ToString(),
                Receiver = null,
                Message = _faker.Lorem.Sentence(),
                SendedAt = _faker.Date.Recent().ToString("yyyy-MM-dd HH:mm:ss")
            }
        },
                InternalAnnotations = new List<InternalAnnotation>
        {
            new InternalAnnotation
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = null,
                Message = _faker.Lorem.Paragraph(),
                CreatedAt = _faker.Date.Recent().ToString("yyyy-MM-dd HH:mm:ss")
            }
        },

                Automations = new Automations
                {
                    Id = Guid.NewGuid().ToString(),
                    FederalRecipe = _faker.Random.Int(0, 1),
                    ContactInfo = _faker.Random.Int(0, 1)
                },

                InteractionHistory = new InteractionHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    Event = _faker.Random.Int(1, 5),
                    From = _faker.Internet.Ip(),
                    To = _faker.Internet.Ip(),
                    CreatedBy = _faker.Person.FullName,
                    CreatedAt = _faker.Date.Recent().ToString("yyyy-MM-dd HH:mm:ss")
                }
            };
        }
    }
}