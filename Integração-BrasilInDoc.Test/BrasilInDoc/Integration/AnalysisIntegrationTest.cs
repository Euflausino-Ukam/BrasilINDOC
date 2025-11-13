using BenchmarkDotNet.Attributes;
using Bogus;
using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
using Newtonsoft.Json;
using Shouldly;
using System.Text;
using Tools;

namespace Integração_BrasilInDoc.Test.BrasilInDoc.Integration
{
    [TestClass]
    [TestCategory("Analisys Integration Test")]
    public class AnalysisIntegrationTest
    {
        private readonly static Faker _faker = new("pt_BR");
        private readonly string apiKey = "apiKeyTest";

        [TestMethod]
        [Benchmark]
        public async Task Create_Analysis_Async_HttpException()
        {
            // Arrange
            var @params = new Helpes.MocksModel.Mocks().CreateClientApllicationValid();
            var jsonClient = JsonConvert.SerializeObject(@params, Formatting.Indented);
            var exception = new HttpRequestException("Falha de conexão com o servidor.");
            var client = Helpes.Handlers.HttpHelperMock.CreateHttpClient(out var handlerMock, ex: exception);

            var service = new AnalysisIntegration(client);

            // Act
            var expectedException = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await service.CreateAnalysisAsync(apiKey, jsonClient);
            });

            // Assert
            expectedException.ShouldNotBeNull();
            expectedException.Errors.ShouldContain("Falha na requisição.");
        }

        [TestMethod]
        [Benchmark]
        public async Task Create_Analysis_Async_HttpStatusCodeFail()
        {
            // Arrange
            var @params = new Helpes.MocksModel.Mocks().CreateClientApllicationValid();
            var jsonClient = JsonConvert.SerializeObject(@params, Formatting.Indented);

            var result = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            var resultMock = Helpes.Handlers.HttpHelperMock.CreateHttpClient
                (out var handlerMock, response: result);

            var service = new AnalysisIntegration(resultMock);

            // Act
            var expectedException = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await service.CreateAnalysisAsync(apiKey, jsonClient);
            });

            // Assert
            expectedException.ShouldNotBeNull();
            expectedException.Errors.ShouldContain("Falha na criação de análise.");
        }

        [TestMethod]
        [Benchmark]
        public async Task Create_Analysis_Async_HttpStatusCodeSucess()
        {
            // Arrange
            var @params = new Helpes.MocksModel.Mocks().CreateClientApllicationValid();
            var jsonClient = JsonConvert.SerializeObject(@params, Formatting.Indented);

            var expectedCod = 1; //?

            var jsonBody = "{\"id\":\"" + @params.ExternalId + "\",\"cod\":" + expectedCod + "}";

            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            var resultMock = Helpes.Handlers.HttpHelperMock.CreateHttpClient(out var handlerMock, response: result);

            var service = new AnalysisIntegration(resultMock);

            // Act
            var expected = await service.CreateAnalysisAsync(apiKey, jsonClient);

            // Assert
            var expectedResult = GetIdByJson.ExtractIdFromJson(expected);

            expectedResult.ShouldNotBeNull();
            expectedResult.ShouldBe(@params.ExternalId);
        }

        [TestMethod]
        [Benchmark]
        public async Task Update_Analysis_Async_HttpException()
        {
            // Arrange
            var @params = new Helpes.MocksModel.Mocks().CreateClientApllicationValid();
            var jsonClient = JsonConvert.SerializeObject(@params, Formatting.Indented);
            var exception = new HttpRequestException("Falha de conexão com o servidor.");

            var client = Helpes.Handlers.HttpHelperMock.CreateHttpClient
                (out var handlerMock, ex: exception);

            var services = new AnalysisIntegration(client);

            // Act
            var expectedException = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await services.UpdateAnalysisAsync(apiKey, jsonClient, @params.ExternalId);
            });

            // Assert
            expectedException.ShouldNotBeNull();
            expectedException.Errors.ShouldContain("Falha na requisição.");
        }

        [TestMethod]
        [Benchmark]
        [DynamicData(nameof(ResultStatusCode))]
        public async Task Update_Analysis_Async_HttpStatusCodeFail(HttpResponseMessage result)
        {
            // Arrange
            var @params = new Helpes.MocksModel.Mocks().CreateClientApllicationValid();
            var jsonClient = JsonConvert.SerializeObject(@params, Formatting.Indented);

            var resultMock = Helpes.Handlers.HttpHelperMock.CreateHttpClient
                (out var handlerMock, response: result);

            var service = new AnalysisIntegration(resultMock);

            // Act
            var expectedException = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await service.UpdateAnalysisAsync(apiKey, jsonClient, @params.ExternalId);
            });

            // Assert
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                expectedException.Errors.ShouldContain($"Análise com ID: {@params.ExternalId} não encontrada");
            else
                expectedException.Errors.ShouldContain($"Falha na atualização.");
        }

        public static IEnumerable<object[]> ResultStatusCode()
        {
            var badRequest = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            var forbiden = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            var notFound = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            var unauthorized = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            var gone = new HttpResponseMessage(System.Net.HttpStatusCode.Gone);
            var conflit = new HttpResponseMessage(System.Net.HttpStatusCode.Conflict);

            yield return new object[] { badRequest };
            yield return new object[] { forbiden };
            yield return new object[] { notFound };
            yield return new object[] { unauthorized };
            yield return new object[] { gone };
            yield return new object[] { conflit };
        }

        [TestMethod]
        [Benchmark]
        public async Task Update_Analysis_Async_HttpStatusCodeSucess()
        {
            // Arrange
            var @params = new Helpes.MocksModel.Mocks().CreateClientApllicationValid();
            var jsonClient = JsonConvert.SerializeObject(@params, Formatting.Indented);

            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { };

            var resultMock = Helpes.Handlers.HttpHelperMock.CreateHttpClient(out var handlerMock, response: result);

            var service = new AnalysisIntegration(resultMock);

            // Act
            var expectedResult = await service.UpdateAnalysisAsync(apiKey, jsonClient, @params.ExternalId);

            // Assert;
            expectedResult.ShouldBeTrue();
        }
    }
}
