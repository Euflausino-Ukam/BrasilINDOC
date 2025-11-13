using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.DependencyInjection;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services;
using Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static async Task Main()
    {
        var service = new ServiceCollection();

        service.AddIntegrationServices();

        using var provider = service.BuildServiceProvider();

        var analysisService = provider.GetRequiredService<IAnalysisService>();
        var fileService = provider.GetRequiredService<IFileService>();
        var analysisRepo = provider.GetRequiredService<IAnalysisRepository>();
        var webhookRepo = provider.GetRequiredService<IWebhookRepository>();


        string apiKey = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2MjczZGI4MS04MDg1LTQ1MzktOTEyMC1lMzQzMTE4MjhkNWIiLCJpc3MiOiI0YTAzMGE3My0xMWJiLTRjNzUtYWVkNS03YmI1YTQ5ZTNlZGEiLCJ0eXAiOiJhcGkta2V5IiwiaWF0IjoxNzYyODA1NzI5LCJleHAiOjQ5MTg1NjU3Mjl9.INWsC-rKgMggEuUP7m6Iiwc32WBnvbc9DxqV7aHLyolEyVCfPRJGiV98LdwsNJ6ObUchbELuThWRQixcMjxrtx62CuEGuPeB6cmtrOaX51ROTw6vwJjws8efv5el51s0u-F7uUn4AuuR5NaAIpyeNqpYPi8mluUtOVLAbrHr2SqAIWGY96R3VNpwMLZr4oeUzabeLpgVX7kyMbw0TaiX-mh4GvJ_YNdyjyQTKME4Sv14fk5rVTZBocnL-HdH2xxBu0iVuSthninC6km9sQcr5Tt8531IxvQbT9FwZUTUZ7sBfv69UhZq4nOYCeHhbFhPw8xuoa9RHEMoOsf8kmGpmw";

        var client = CreateClientAnalysis();
        var updatedClient = UpdateClientAnalisys();

        var files = new List<FilesArchives>
        {
            new("doc_front", @"C:\Users\Ajinsoft\Pictures\imgs\identity_test.jpg"),
            new("doc_back", @"C:\Users\Ajinsoft\Pictures\imgs\identity_test.jpg"),
            new("selfie", @"C:\Users\Ajinsoft\Pictures\imgs\identity_test.jpg"),
        };

        try
        {
<<<<<<< HEAD
<<<<<<< HEAD
=======
            Guid id = Guid.NewGuid();
>>>>>>> bb1170ac41330a9435bc733df7ba76647fdfb690
=======
>>>>>>> 15f6e20a07af79c654ea32e63d3246e331ad4fb9
            await analysisService.UpdateClientStatusAsync();
            //await analysisService.CreateClientAnalysisAsync(apiKey, client, files);
        }
        catch (ErrorLists ex)
        {
            Console.WriteLine(ex);
        }

    }

    public static ClientParams CreateClientAnalysis()
    {
        return new ClientParams
        {
            ProposalNumber = "PRP-001",
            ProposalValue = 15000.75,
            PartnerCod = "998877",
            Type = 1,
            ClientName = "Claudio Henrique",
            ClientCpf = "228.690.910-52",
            ClientBirthDate = "1990-05-15",
            PerformanceSquare = "Zona Sul",
            Phone = "(21)999999999",
            Branch = "Centro-SP",
            State = "SP",
            Formalization = 1,
            DocumentType = 2,
            ExpeditionDate = "2022-10-01",
            HasSelfie = false,
            Message = "Atualização de informações do cliente e anexos"
        };
    }

    public static ClientParams UpdateClientAnalisys()
    {
        return new ClientParams
        {
            ProposalNumber = "PRP-001",
            ProposalValue = 15000.75,
            PartnerCod = "998877",
            Type = 1,
            ClientName = "Claudio Henrique",
            ClientCpf = "666.263.078-00",
            ClientBirthDate = "1990-05-15",
            PerformanceSquare = "Zona Sul",
            Phone = "(21)999999999",
            Branch = "Centro-RJ",
            State = "RJ",
            Formalization = 1,
            DocumentType = 2,
            ExpeditionDate = "2024-10-01",
            HasSelfie = false,
            Message = "Atualização de informações do cliente e anexos"
        };
    }
}