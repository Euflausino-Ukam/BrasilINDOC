using System.Text.Json;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration
{
    public interface IAnalysisIntegration
    {
        Task<JsonDocument> CreateAnalysisAsync(string apiKey, string json);
        Task<Stream> GetAnalysisAsync(string apiKey, string id);
        Task<bool> UpdateAnalysisAsync(string apiKey, string json, string? id);
    }
}