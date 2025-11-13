using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Webhooks;
using Integração_BrasilInDoc.BrasilInDoc.Domain;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
using Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client;
using Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;
using Tools;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IFileService _fileService;
        private readonly IAnalysisIntegration _analysisIntegration;
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IWebhookRepository _webhookRepository;

        public AnalysisService
            (
                IFileService fileService,
                IAnalysisIntegration analysisIntegration,
                IAnalysisRepository analysisRepository,
                IWebhookRepository webhookRepository
            )
        {
            _fileService = fileService;
            _analysisIntegration = analysisIntegration;
            _analysisRepository = analysisRepository;
            _webhookRepository = webhookRepository;
        }

        public async Task<string> CreateClientAnalysisAsync
            (
                string apiKey,
                ClientParams client,
                List<FilesArchives> files
            )
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(apiKey))
                errors.Add("A chave de API não pode ser nula ou vazia");

            if (client is null)
                errors.Add("O cliente não pode ser nulo");
            else
                client!.IsValid();

            if (files is null)
                errors.Add("É necessario o envio dos arquivos para análise");

            if (errors.Count > 0)
                throw new ErrorLists(errors);

            client!.HasSelfie = files!.Any(f => f.Type == "selfie");

            client!.Attachments = await _fileService.UploadImagesAsync(apiKey, files!);

            var json = JsonSerializer.Serialize(client);

            var jsonResult = await _analysisIntegration.CreateAnalysisAsync(apiKey, json);

            var resultId = GetIdByJson.ExtractIdFromJson(jsonResult);

            var clientResult = new ClientApplication();

            clientResult.CreateClientCreate(client);

            clientResult.ExternalId = resultId;

            clientResult.RequestPayload!.Add(json);
            clientResult.ResponsePayload!.Add(jsonResult.ToString()!);

            //Salva no BD - teste
            await _analysisRepository.CreateAnalysisAsync(clientResult);
            //

            return resultId!;
        }

        public async Task<ClientApplication> UpdateClientAnalysisAsync
            (
                string apiKey,
                ClientParams @params,
                List<FilesArchives> files
            )
        {
            var errors = new List<string>();

            var banco = new ClientApplication();

            if (string.IsNullOrWhiteSpace(apiKey))
                errors.Add("A chave de API não pode ser nula ou vazia");

            if (@params is null)
                errors.Add("O cliente não pode ser nulo");
            else
            {
                @params!.IsValid();

                var identity = @params.ClientCpf ?? @params.ClientCnpj;

                banco = await _analysisRepository.GetClientByIdentityAnalysisAsync(identity!);
            }

            if (banco is null || banco == new ClientApplication())
                errors.Add("Cliente não encontrado para Update");

            if (files is null && !banco!.Attachments!.Any())
                errors.Add("É necessario o envio dos arquivos para análise");

            if (errors.Count > 0)
                throw new ErrorLists(errors);

            if (files!.Count != 0)
                @params!.UpdateAttachments(await _fileService.UploadImagesAsync(apiKey, files));

            var json = JsonSerializer.Serialize(@params);

            // Faz a busca para encontrar o ID no BrasilInDoc

            await _analysisIntegration.UpdateAnalysisAsync(apiKey, json, banco!.ExternalId);

            banco.UpdateClient(@params!);

            banco.RequestPayload.Add(json!.ToString());

            //Salva no BD
            await _analysisRepository.UpdateClientAnalysisAsync(banco);
            //

            return banco;
        }

        public async Task<ClientResult> GetClientAnalysisAsync
            (
                string apiKey,
                Guid clientId
            )
        {
            if (Guid.Empty == clientId)
                throw new ErrorLists(["O Id do cliente não pode ser vazio"]);

            var id = clientId.ToString();

            using var body = await _analysisIntegration.GetAnalysisAsync(apiKey, id);

            var clientResult = await JsonSerializer.DeserializeAsync<ClientResult>(body);

            if (clientResult is null)
                throw new ErrorLists([$"Falha ao tratar a resposta da análise de ID: {id}"]);

            var client = new ClientApplication();

            client.UpdateClient(clientResult);
            client.ResponsePayload!.Add(body.ToString()!);

            //Salva no BD

            //

            return clientResult!;
        }

        public async Task UpdateClientStatusAsync()
        {
            Console.WriteLine("Iniciando atualização de status.");

            while (true)
            {
                var webhook = await _webhookRepository.GetWebhookAsync();

                if (webhook is null)
                {
                    Console.WriteLine("Nenhum webhook encontrado.");
                    Console.Write("Aguardando");
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write(".");
                        await Task.Delay(1000);
                    }
                    Console.WriteLine("\n");
                    await Task.Delay(25000);
                    continue;
                }

                if (webhook!.Data.ExternalId is null || string.IsNullOrWhiteSpace(webhook.Data.ExternalId))
                {
                    Console.WriteLine("Id não informado no webhook.");
                    webhook.Flag = true;
                    Console.WriteLine("\n");
                    await _webhookRepository.UpdateWebhookAsync(webhook);
                    continue;
                }

                Console.WriteLine($"Webhook ID: {webhook.Id}");

                var client = await _analysisRepository.GetClientAnalysisAsync(webhook.Data.ExternalId);

                if (client is null)
                {
                    Console.WriteLine("Cliente não encontrado. \n");
                    webhook.Flag = true;
                    await _webhookRepository.UpdateWebhookAsync(webhook);
                    continue;
                }

                if (webhook.ReceiverDate >= client.UpdatedAt || client.UpdatedAt is null)
                {
                    Console.WriteLine($"Cliente: {client.ClientName}.");
                    if (client.ClientCpf is not null) Console.WriteLine($"CPF: {client.ClientCpf}");
                    else Console.WriteLine($"CPF: {client.ClientCpf}");
                    Console.WriteLine($"Status: {Enum.GetName(typeof(StatusType), webhook.Data.Status)}");
                    client.Status = (StatusType)webhook.Data.Status;
                    await _analysisRepository.UpdateClientAnalysisAsync(client);
                }
                else
                {
                    Console.WriteLine($"Cliente: ${client.ClientName} já esta atualizado.");
                }

                webhook.Flag = true;
                await _webhookRepository.UpdateWebhookAsync(webhook);

                Console.WriteLine("\n-------------------------------------------------");
            }
        }
    }
}
