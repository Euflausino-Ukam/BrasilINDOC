using Microsoft.AspNetCore.Http;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services
{
    public interface IWebhookServices
    {
        Task WebhookAsync(HttpRequest req);
    }
}