using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;

namespace Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments
{
    public class AttachmentTypes
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public int? AttachType { get; set; }

        public AttachmentTypes() { }

        public AttachmentTypes(string Name, int attachType)
        {
            this.Name = Name;
            this.AttachType = attachType;
        }

        public AttachmentTypes(AttachmentParams @params) 
        { 
            this.Id = @params.FileId;
            this.Name = TypeToString(@params.AttachType ?? 4);
            this.AttachType = @params.AttachType;
        }

        public AttachmentTypes(string Name, string attachType)
        {
            this.Name = Name;
            this.AttachType = TypeToInt(attachType);
        }
        public AttachmentTypes(string id, string Name, int attachType)
        {
            this.Id = id;
            this.Name = Name;
            this.AttachType = attachType;
        }
        public AttachmentTypes(string id, string Name, string attachType)
        {
            this.Id = id;
            this.Name = Name;
            this.AttachType = TypeToInt(attachType);
        }

        public int? TypeToInt(string attachType)
        {
            return attachType.ToLower() switch
            {
                "doc_front" => 1,
                "doc_back" => 2,
                "selfie" => 3,
                _ => 4,
            };
        }

        public string? TypeToString(int? attachType)
        {
            return attachType switch
            {
                1 => "doc_front",
                2 => "doc_back",
                3 => "selfie",
                _ => "outher",
            };
        }
    }

    public class Attachment
    {
        public static readonly AttachmentTypes Front = new AttachmentTypes("doc_front", 1);
        public static readonly AttachmentTypes Back = new AttachmentTypes("doc_back", 2);
        public static readonly AttachmentTypes Selfie = new AttachmentTypes("selfie", 3);
        public static readonly AttachmentTypes Outher = new AttachmentTypes("outher", 4);
        
        public static IEnumerable<AttachmentTypes> List() => new[] { Front, Back, Selfie, Outher };
    }
}
