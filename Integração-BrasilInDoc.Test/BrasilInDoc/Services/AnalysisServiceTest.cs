using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.Client;
using Integração_BrasilInDoc.BrasilInDoc.Apllication.Dto.File;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services;
using Integração_BrasilInDoc.BrasilInDoc.Models.Domain.Client;
using Integração_BrasilInDoc.BrasilInDoc.RepositoryLocal;
using Moq;
using Shouldly;
using System.Text.Json;
using DescriptionAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;

namespace Integração_BrasilInDoc.Test.BrasilInDoc.Services
{
    [TestClass]
    [TestCategory("Analisys Services Test")]
    public class AnalysisServiceTest
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<IAnalysisIntegration> _analysisIntegrationMock;
        private readonly Mock<IAnalysisRepository> _repositoryMock;
        private readonly Mock<IWebhookRepository> _webhookMock;

        private readonly AnalysisService analysisService;

        public AnalysisServiceTest() 
        {
            _fileServiceMock = new Mock<IFileService>();
            _analysisIntegrationMock = new Mock<IAnalysisIntegration>();
            _repositoryMock = new Mock<IAnalysisRepository>();
            _webhookMock = new Mock<IWebhookRepository>();

            analysisService = new AnalysisService
                (_fileServiceMock.Object, _analysisIntegrationMock.Object, _repositoryMock.Object, _webhookMock.Object);
        }



        [TestMethod]
        [DynamicData(nameof(GetErrorDataTest))]
        [Description("CreateClientAnalysis - Falha na criação do teste unitario")]
        public async Task Create_Cliente_Analysis_Async_Fail
            (
                string expectApiKey,
                ClientParams expectedClient,
                List<FilesArchives> expectedFiles
            )
        {
            // Arrange

            // Act
            var exception = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await analysisService.CreateClientAnalysisAsync(expectApiKey, expectedClient, expectedFiles);
            });

            // Assert
            exception.ShouldNotBeNull();
            exception.Errors.ShouldNotBeEmpty();

            if (string.IsNullOrWhiteSpace(expectApiKey))
                exception.Errors.ShouldContain("A chave de API não pode ser nula ou vazia");

            if (expectedClient is null)
                exception.Errors.ShouldContain("O cliente não pode ser nulo");

            if (exception.Errors.Contains("A identificação do cliente não é valida."))
                exception.Errors.ShouldContain("O nome do cliente não é valido.");

            if (expectedFiles == null || expectedFiles.Count < 2)
                exception.Errors.ShouldContain("É necessario o envio dos arquivos para análise");
        }
        public static IEnumerable<object[]> GetErrorDataTest()
        {
            var modelFakes = new Helpes.MocksModel.Mocks();
            var validClient = modelFakes.CreateClientParamsValid();
            var invalidClient = modelFakes.CreateClientParamsInvalid();
            var files = modelFakes.CreateAttamentParamsValid();


            yield return new object[] { null, validClient, files };
            yield return new object[] { "apikey", invalidClient, files };
            yield return new object[] { "apikey", null, files };
            yield return new object[] { "apikey", validClient, null };
            yield return new object[] { "", validClient, null };
        }

        [TestMethod]
        [DynamicData(nameof(GetDataToTest))]
        [Description ("CreateClientAnalysis - Sucesso na criação da análise do cliente")]
        public async Task Create_Cliente_Analysis_Async_Sucess
            (
                string expectedApiKey,
                ClientParams expectedClient,
                List<FilesArchives> expectedFiles
            )
        {
            // Arrange
            var attachments = new List<AttachmentParams>();

            var expectedId = Guid.NewGuid();

            var fakeJson = JsonDocument.Parse("{\"id\":\"" + $"{expectedId}" +"\"}");

            foreach (var archive in expectedFiles)
                attachments.Add(new AttachmentParams(Guid.NewGuid().ToString(), archive.Type));

            _fileServiceMock
                .Setup(x => x.UploadImagesAsync(It.IsAny<string>(), It.IsAny<List<FilesArchives>>()))
                .ReturnsAsync(attachments);

            _analysisIntegrationMock
                .Setup(x => x.CreateAnalysisAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(fakeJson);

            // Act
            var expectedResult = await analysisService.CreateClientAnalysisAsync(expectedApiKey, expectedClient, expectedFiles);

            // Assert
            expectedResult.ShouldNotBeNull();
            expectedResult.ShouldBe(expectedId.ToString());
        }
        public static IEnumerable<object[]> GetDataToTest()
        {
            var modelFakes = new Helpes.MocksModel.Mocks();
            var validClient = modelFakes.CreateClientParamsValid();
            var filesWithSelfie = modelFakes.CreateAttamentParamsValid();
            var filesWithoutSelfie = modelFakes.CreateAttamentParamsWithoutSelfieValid();

            yield return new object[] { "apikey", validClient, filesWithSelfie };
            yield return new object[] { "apikey", validClient, filesWithoutSelfie };
        }

        [TestMethod]
        [DynamicData(nameof(GetInvalidDataToUpdate))]
        [Description("UpdateClientAnalysisAsync - Dados para Update do cliente invalidos")]
        public async Task Update_Client_Invalid_Params
            (
                string expectApiKey, 
                ClientApplication clientBd, 
                ClientParams expectedClient,
                List<FilesArchives> files
            )
        {
            // Arrange
            if (expectApiKey is not null && expectedClient is not null)
                _repositoryMock.Setup
                    (x => x.GetClientByIdentityAnalysisAsync(It.IsAny<string>())).ReturnsAsync(clientBd);

            // Act
            var exceptionExpected = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await analysisService.UpdateClientAnalysisAsync(expectApiKey, expectedClient, files);
            });

            // Assert
            if (string.IsNullOrWhiteSpace(expectApiKey))
                exceptionExpected.Errors.ShouldContain("A chave de API não pode ser nula ou vazia");

            if (expectedClient is null)
                exceptionExpected.Errors.ShouldContain("O cliente não pode ser nulo");

            if (exceptionExpected.Errors.Contains("A identificação do cliente não é valida."))
                exceptionExpected.Errors.ShouldContain("O nome do cliente não é valido.");

            if (files == null || files.Count < 2)
                exceptionExpected!.Errors.ShouldContain("É necessario o envio dos arquivos para análise");
        }

        public static IEnumerable<object[]> GetInvalidDataToUpdate() 
        {
            var modelFakes = new Helpes.MocksModel.Mocks();

            var clientDb = modelFakes.CreateClientApllicationValid();
            var clientValidUpdate = modelFakes.CreateClientParamsValid();
            var clientInvalidUpdate = modelFakes.CreateClientParamsInvalid();
            var files = modelFakes.CreateAttamentParamsValid();

            yield return new object[] { null, clientDb, clientValidUpdate, files };
            yield return new object[] { "apiKey", null, clientValidUpdate, files };
            yield return new object[] { "apiKey", clientDb, null, files };
            yield return new object[] { "apiKey", clientDb, clientInvalidUpdate, files };
            yield return new object[] { "apiKey", clientDb, clientValidUpdate, null };
        }

        [TestMethod]
        [DynamicData(nameof(GetValidDataToUpdate))]
        [Description("UpdateClientAnalysisAsync - Dados validos para seguir com o update")]
        public async Task Update_Client_Valid_Params
            (
                string expectedKey, 
                ClientApplication clientBd, 
                ClientParams @params, 
                List<FilesArchives> expectedFile
            )
        {
            // Arrange
            var attachments = new List<AttachmentParams>();

            foreach (var archive in expectedFile)
                attachments.Add(new AttachmentParams(Guid.NewGuid().ToString(), archive.Type));

            _fileServiceMock.Setup
                (x => x.UploadImagesAsync(expectedKey, expectedFile))
                .ReturnsAsync(attachments);

            _analysisIntegrationMock.Setup
                (x => x.UpdateAnalysisAsync(expectedKey, It.IsAny<string>(), clientBd.ExternalId));

            _repositoryMock.Setup
                (x => x.GetClientByIdentityAnalysisAsync(It.IsAny<string>())).ReturnsAsync(clientBd);

            // Act
            var expectedResult = await analysisService.UpdateClientAnalysisAsync(expectedKey, @params, expectedFile);

            // Assert

            expectedResult.ShouldNotBeNull();
            expectedResult.ExternalId.ShouldBe(clientBd.ExternalId);
        }

        public static IEnumerable<object[]> GetValidDataToUpdate()
        {
            var modelFakes = new Helpes.MocksModel.Mocks();

            var clientDb = modelFakes.CreateClientApllicationValid();
            var clientValidUpdate = modelFakes.CreateClientParamsValid();
            var filesWithSelfie = modelFakes.CreateAttamentParamsValid();
            var filesWithoutSelfie = modelFakes.CreateAttamentParamsWithoutSelfieValid();

            yield return new object[] { "apiKey", clientDb, clientValidUpdate, filesWithSelfie };
            yield return new object[] { "apiKey", clientDb, clientValidUpdate, filesWithoutSelfie };
        }
    }
}
