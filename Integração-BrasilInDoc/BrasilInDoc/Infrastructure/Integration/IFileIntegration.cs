namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration
{
    public interface IFileIntegration
    {
        Task DeleteFileAsync(string fileId);
        Task DownloadFileAsync(string apiKey, string fileId);
        Task DownloadManiFileAsync();
        Task<string> UploadFileAsync(string apiKey, string filePath, string receiver);
    }
}