using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IFileIntegration _fileIntegration;

        public FileService(IFileIntegration fileIntegration)
        {
            _fileIntegration = fileIntegration;
        }

        public async Task<List<AttachmentParams>> UploadImagesAsync
            (
                string apiKey,
                List<FilesArchives> files
            )
        {
            var errors = new List<string>();

            var attachments = new List<AttachmentParams>();
            if (apiKey is null || string.IsNullOrWhiteSpace(apiKey))
                throw new ErrorLists(["Chave da Api não encontrada."]);

            if (files is null)
                throw new ErrorLists(["Nenhum aquivo encontrado."]);

            foreach (var file in files)
            {
                if (!Attachment.List().Any(a => a.Name.Equals(file.Type, StringComparison.OrdinalIgnoreCase)))
                {
                    errors.Add("Tipo de arquivo invalido ou não encontrado");
                    continue;
                }

                try
                {
                    var attachmentId = await _fileIntegration
                        .UploadFileAsync(apiKey, file.UrlFile, "qualiconsig");

                    if (string.IsNullOrWhiteSpace(attachmentId))
                        errors.Add($"Falha no upload do arquivo: {file.Type}");

                    else
                        attachments.Add(new AttachmentParams(attachmentId, file.Type));
                }
                catch (ErrorLists ex)
                {
                    errors.Add($"{ex}");
                }
            }

            if (attachments.Count != files.Count)
                throw new ErrorLists(errors);

            return attachments;
        }
    }
}
