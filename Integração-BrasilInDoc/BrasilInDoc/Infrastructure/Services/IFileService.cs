using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services
{
    public interface IFileService
    {
        Task<List<AttachmentParams>> UploadImagesAsync(string apiKey, List<FilesArchives> files);
    }
}