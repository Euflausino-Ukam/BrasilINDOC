using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Webhooks;

namespace Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal
{
    public interface IWebhookRepository
    {
        Task SaveWebhookAsync(Webhook webhook);
        Task<Webhook> GetWebhookAsync();
        Task UpdateWebhookAsync(Webhook webhook);
    }
}