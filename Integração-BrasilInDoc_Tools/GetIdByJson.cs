using Errors;
using System.Text.Json;

namespace Tools
{
    public class GetIdByJson
    {
        public static string ExtractIdFromJson(JsonDocument jsonResult)
        {

            var root = jsonResult.RootElement;

            static string? ElementToString(JsonElement element)
            {
                return element.ValueKind switch
                {
                    JsonValueKind.String => element.GetString(),
                    JsonValueKind.Number => element.GetRawText(),
                    JsonValueKind.Array => string.Join(", ", element.EnumerateArray()
                                                        .Select(ElementToString)
                                                        .Where(s => s is not null)),
                    _ => throw new ErrorLists(["Nenhum retorno durante a solicitação de análise."]),
                };
            }

            string? fileId = null;

            if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("id", out var pId))
                    fileId = ElementToString(pId);

                if (string.IsNullOrWhiteSpace(fileId) && root.TryGetProperty("ids", out var pIds))
                    fileId = ElementToString(pIds);

                if (string.IsNullOrWhiteSpace(fileId) && root.TryGetProperty("fileId", out var pFileId))
                    fileId = ElementToString(pFileId);

                if (string.IsNullOrWhiteSpace(fileId) && root.TryGetProperty("fieldsId", out var pFieldsId))
                    fileId = ElementToString(pFieldsId);
            }

            if (string.IsNullOrWhiteSpace(fileId)
                && root.ValueKind == JsonValueKind.Object
                && root.TryGetProperty("data", out var dataEl)
                && dataEl.ValueKind == JsonValueKind.Object)
            {
                if (dataEl.TryGetProperty("id", out var dId))
                    fileId = ElementToString(dId);

                if (string.IsNullOrWhiteSpace(fileId) && dataEl.TryGetProperty("ids", out var dIds))
                    fileId = ElementToString(dIds);

                if (string.IsNullOrWhiteSpace(fileId) && dataEl.TryGetProperty("fileId", out var dFileId))
                    fileId = ElementToString(dFileId);

                if (string.IsNullOrWhiteSpace(fileId) && dataEl.TryGetProperty("fieldsId", out var dFieldsId))
                    fileId = ElementToString(dFieldsId);
            }

            return fileId ?? throw new ErrorLists(["Nenhum ID encontrado na resposta JSON."]);
        }
    }
}
