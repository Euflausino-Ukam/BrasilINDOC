using Errors;
using System.Text;
using System.Text.Json;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration
{
    public class AnalysisIntegration : IAnalysisIntegration
    {
        private static readonly string urlBase = "https://api.indoc-h.brasilindoc.com.br";
        private static readonly string endpoint = "/document-analysis";

        private readonly HttpClient _httpClient;

        public AnalysisIntegration (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JsonDocument> CreateAnalysisAsync(string apiKey, string json)
        {
            try
            {
                using var request = new HttpRequestMessage
                    (
                         HttpMethod.Post,
                        $"{urlBase}{endpoint}"
                     );

                request.Headers.Add("ApiKey", apiKey);

                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new ErrorLists([$"Falha na criação de análise."]);

                return JsonDocument.Parse(body);
            }
            catch (HttpRequestException ex)
            {
                throw new ErrorLists(["Falha na requisição."]);
            }
            catch (TaskCanceledException)
            {
                throw new ErrorLists([$"Solicitação de analise cancelada."]);
            }
            catch (ErrorLists ex)
            {
                throw new ErrorLists(ex.Errors);
            }
            catch (Exception)
            {
                throw new ErrorLists([$"Erro inesperado ao realizar a solicitação de análise"]);
            }
        }

        public async Task<bool> UpdateAnalysisAsync(string apiKey, string json, string? id)
        {
            try
            {
                using var request = new HttpRequestMessage
                    (
                        HttpMethod.Put,
                        $"{urlBase}{endpoint}/{id}"
                    );

                request.Headers.Add("ApiKey", apiKey);

                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new ErrorLists([$"Análise com ID: {id} não encontrada"]);

                if (!response.IsSuccessStatusCode)
                    throw new ErrorLists([$"Falha na atualização."]);

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new ErrorLists([$"Falha na requisição."]);
            }
            catch (TaskCanceledException)
            {
                throw new ErrorLists([$"Solicitação de analise cancelada."]);
            }
            catch (ErrorLists ex)
            {
                throw new ErrorLists(ex.Errors);
            }
            catch (Exception)
            {
                throw new ErrorLists([$"Erro inesperado ao realizar a solicitação de análise"]);
            }
        }

        public async Task<Stream> GetAnalysisAsync(string apiKey, string id)
        {
            try
            {
                using var request = new HttpRequestMessage
                    (
                        HttpMethod.Get,
                        $"{urlBase}{endpoint}/{id}"
                    );

                request.Headers.Add("ApiKey", apiKey);

                var httpClient = new HttpClient();

                using var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    throw new ErrorLists([$"Falha ao obter a análise com ID: {id}"]);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new ErrorLists([$"Análise com ID: {id} não encontrada"]);

                return await response.Content.ReadAsStreamAsync();
            }
            catch (FormatException)
            {
                throw new ErrorLists(["ID de análise inválido."]);
            }
            catch (HttpRequestException ex)
            {
                throw new ErrorLists(["Falha na requisição: " + ex.Message]);
            }
            catch (TaskCanceledException)
            {
                throw new ErrorLists(["Solicitação de análise cancelada."]);
            }
            catch (ErrorLists ex)
            {
                throw new ErrorLists(ex.Errors);
            }
            catch (Exception)
            {
                throw new ErrorLists(["Erro interno."]);
            }
        }
    }
}
