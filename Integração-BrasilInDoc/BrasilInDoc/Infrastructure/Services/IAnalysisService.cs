using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client;
using Microsoft.AspNetCore.Http;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services
{
    public interface IAnalysisService
    {
        Task<string> CreateClientAnalysisAsync(string apiKey, ClientParams client, List<FilesArchives> files);
        Task<ClientResult> GetClientAnalysisAsync(string apiKey, Guid clientId);
        Task<ClientApplication> UpdateClientAnalysisAsync(string apiKey, ClientParams @params, List<FilesArchives> files);
        Task UpdateClientStatusAsync();
    }
}