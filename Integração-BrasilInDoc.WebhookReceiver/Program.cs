
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.DependencyInjection;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIntegrationServices();

var app = builder.Build();

app.MapGet("/", () => "Teste local server.");

app.MapPost("/webhook", async
    (
        HttpRequest req,
        IWebhookServices _webhookService
    ) =>
{
    try
    {
        await _webhookService.WebhookAsync(req);
        return;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return;
    }
});

app.Run();
