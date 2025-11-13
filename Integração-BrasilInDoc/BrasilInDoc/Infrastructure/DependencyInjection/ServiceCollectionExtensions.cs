using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services;
using Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal;
using Microsoft.Extensions.DependencyInjection;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices
            (
                this IServiceCollection services
            )
        {
            services.AddTransient<IFileService, FileService>();
            services.AddHttpClient<IFileIntegration, FileIntegration>();

            services.AddTransient<IAnalysisService, AnalysisService>();
            services.AddHttpClient<IAnalysisIntegration, AnalysisIntegration>();
            services.AddSingleton<IAnalysisRepository, AnalysisRepository>();

            services.AddSingleton<IWebhookServices, WebhookServices>();
            services.AddSingleton<IWebhookRepository, WebhookRepository>();

            return services;
        }
    }
}
