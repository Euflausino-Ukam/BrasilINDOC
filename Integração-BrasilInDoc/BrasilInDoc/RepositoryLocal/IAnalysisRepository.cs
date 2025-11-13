using Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client;

namespace Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal
{
    public interface IAnalysisRepository
    {
        Task CreateAnalysisAsync(ClientApplication clientResult);
        Task<ClientApplication?> GetClientAnalysisAsync(string id);
        Task UpdateClientAnalysisAsync(ClientApplication clientResult);
        Task<ClientApplication?> GetClientByIdentityAnalysisAsync(string identity);
    }
}