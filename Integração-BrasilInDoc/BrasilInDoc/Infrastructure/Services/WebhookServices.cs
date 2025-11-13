using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Webhooks;
using Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services
{
    public class WebhookServices : IWebhookServices
    {
        private readonly IWebhookRepository _webhookRepository;
        public WebhookServices(IWebhookRepository webhookRepository)
        {
            _webhookRepository = webhookRepository;
        }
        public async Task WebhookAsync
            (
                HttpRequest req
            )
        {
            using var reader = new StreamReader(req.Body, Encoding.UTF8);
            var json = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(json))
                throw new ErrorLists(["Webhook nulo."]);

            var result = new Webhook();
                
            result = JsonSerializer.Deserialize<Webhook>(json);
           
            result!.Payload = json;

            await _webhookRepository.SaveWebhookAsync(result!);
        }
    }
}
