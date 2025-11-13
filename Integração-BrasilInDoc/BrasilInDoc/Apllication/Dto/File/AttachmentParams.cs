using System.Text.Json.Serialization;

namespace Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File
{
    public class AttachmentParams
    {
        [JsonPropertyName("fileId")]
        public string? FileId { get; set; }

        [JsonPropertyName("attachType")]
        public int? AttachType { get; set; }

        public AttachmentParams() { }

        public AttachmentParams(string fileId, int attachType)
        {
            this.FileId = fileId;
            this.AttachType = attachType;
        }

        public AttachmentParams(AttachmentParams @params)
        {
            this.FileId = @params.FileId;
            this.AttachType = @params.AttachType;
        }

        public AttachmentParams(string fileId, string attachType)
        {
            this.FileId = fileId;
            this.AttachType = TypeToInt(attachType);
        }

        public int TypeToInt(string attachType)
        {
            return attachType.ToLower() switch
            {
                "doc_front" => 1,
                "doc_back" => 2,
                "selfie" => 3,
                _ => 4,
            };
        }
    }
}
