using Errors;
using System.Net.Http.Headers;
using System.Text.Json;
using Tools;

namespace Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration
{
    public class FileIntegration : IFileIntegration
    {
        private static readonly string baseUrl = "https://api.file-manager-h.brasilindoc.com.br";
        private static readonly string endpoint = "/file";

        private readonly HttpClient _httpClient;

        public FileIntegration(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadFileAsync
            (
                string apiKey,
                string filePath,
                string receiver
            )
        {
            if (filePath is null)
                throw new ErrorLists(["Caminho para o arquivo nulo"]);

            var fileName = Path.GetFileName(filePath);
            var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
            var convertPdfToImage = fileExtension == ".pdf";

            try
            {
                await using var fileStream = File.OpenRead(filePath);
                using var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(fileStream);

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(MimeType.GetMimeType(fileExtension));

                content.Add(fileContent, "file", fileName);
                content.Add(new StringContent(convertPdfToImage.ToString().ToLower()), "convertPdfToImage");

                // Verificar se o receiver pode ser nulo ou vazio
                //content.Add(new StringContent(receiver), "receiver");

                using var request = new HttpRequestMessage
                    (
                        HttpMethod.Post,
                        $"{baseUrl}{endpoint}"
                    );

                request.Headers.Add("ApiKey", apiKey);

                request.Content = content;

                using var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new ErrorLists([$"Falha no upload do arquivo: {body}"]);

                var fileId = GetIdByJson.ExtractIdFromJson(JsonDocument.Parse(body));

                return fileId!;
            }
            catch (DirectoryNotFoundException)
            {
                throw new ErrorLists([$"Diretório não encontrado ao tentar acessar o arquivo: {filePath}"]);
            }
            catch (ArgumentException)
            {
                throw new ErrorLists([$"Diretório não encontrado ao tentar acessar o arquivo: {filePath}"]);
            }
            catch (FileNotFoundException)
            {
                throw new ErrorLists([$"Arquivo não encontrado: {fileName}"]);
            }
            catch (HttpRequestException)
            {
                throw new ErrorLists([$"Falha ao fazer upload do arquivo: {fileName}"]);
            }
            catch (TaskCanceledException)
            {
                throw new ErrorLists([$"Upload do arquivo cancelado: {fileName}"]);
            }
            catch (ErrorLists ex)
            {
                throw new ErrorLists(ex.Errors);
            }
            catch (Exception)
            {
                throw new ErrorLists([$"Erro inesperado ao fazer upload do arquivo: {fileName}"]);
            }
        }

        public async Task DownloadFileAsync
            (
                string apiKey,
                string fileId
            )
        {
            try
            {
                using var request = new HttpRequestMessage
                    (
                        HttpMethod.Get,
                        $"{baseUrl}{endpoint}/{fileId}/download"
                    );

                request.Headers.Add("ApiKey", apiKey);

                var httpClient = new HttpClient();

                using var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new ErrorLists([$"Falha ao baixar o arquivo com ID: {fileId}"]);
            }
            catch
            {

            }
        }

        public async Task DeleteFileAsync(string fileId)
        {
            // Implement file deletion logic here
        }

        public async Task DownloadManiFileAsync()
        {
            // Implement logic to list files here
        }
    }
}
