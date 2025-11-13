using Errors;
using Integração_BrasilInDoc.BrasilInDoc.Domain.Entities.Attachments;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Services;
using Moq;
using Shouldly;

namespace Integração_BrasilInDoc.Test.BrasilInDoc.Services
{
    [TestClass]
    [TestCategory("File Services Test")]
    public class FileServiceTest
    {
        private readonly Mock<IFileIntegration> _fileIntegrationMock;
        private readonly IFileService _fileService;

        public FileServiceTest()
        {
            _fileIntegrationMock = new Mock<IFileIntegration>();

            _fileService = new FileService(_fileIntegrationMock.Object);
        }

        [TestMethod]
        [DynamicData(nameof(UpdateFilesErrors))]
        [Description("E")]
        public async Task Upload_File_Image_Async_Error
            (
                string expectedApiKey,
                List<FilesArchives> expectedFiles
            )
        {
            // Arrange

            _fileIntegrationMock.Setup
                (x => x.UploadFileAsync( 
                    expectedApiKey,
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync((string)null);

            // Act
            var exeptionExpected = await Assert.ThrowsAsync<ErrorLists>(async () =>
            {
                await _fileService.UploadImagesAsync(expectedApiKey, expectedFiles);
            });

            // Assert
            if (string.IsNullOrWhiteSpace(expectedApiKey))
                exeptionExpected.Errors.ShouldContain("Chave da Api não encontrada.");
            
            if (expectedFiles is null)
                exeptionExpected.Errors.ShouldContain("Nenhum aquivo encontrado.");

            if (expectedFiles != null && !string.IsNullOrWhiteSpace(expectedApiKey)
                && expectedFiles.Any(f => !Attachment.List().Any(a => a.Name.Equals(f.Type, StringComparison.OrdinalIgnoreCase))))
                exeptionExpected.Errors.Contains("Tipo de arquivo invalido ou não encontrado");
        }

        public static IEnumerable<object[]> UpdateFilesErrors()
        {
            var mocks = new Helpes.MocksModel.Mocks();
            var validFiles = mocks.CreateAttamentParamsValid();
            var invalidFiles = mocks.CreateAttamentParamsInvalid();

            yield return new object[] { null, validFiles };
            yield return new object[] { "", validFiles };
            yield return new object[] { "apiKey", invalidFiles };
            yield return new object[] { "apiKey", null };
            yield return new object[] { "apiKey", validFiles };
        }

        [TestMethod]
        [DynamicData(nameof(UpdateFilesSucess))]
        public async Task Upload_File_Image_Async_Sucess
            (
                string expectedApiKey, 
                List<FilesArchives> expectedFiles
            )
        {
            // Arrange

            _fileIntegrationMock.Setup
                (x => x.UploadFileAsync(
                    expectedApiKey,
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(Guid.NewGuid().ToString());

            // Act
            var expectedResult = await _fileService.UploadImagesAsync(expectedApiKey, expectedFiles);

            // Assert

            expectedResult.ShouldNotBeNull();
            expectedResult.Any(e => !expectedFiles.Any(f => f.Type == new AttachmentTypes().TypeToString(e.AttachType)));
        }

        public static IEnumerable<object[]> UpdateFilesSucess()
        {
            var mocks = new Helpes.MocksModel.Mocks();
            var validFilesSelfie = mocks.CreateAttamentParamsValid();
            var validFilesWithoutSelfie = mocks.CreateAttamentParamsWithoutSelfieValid();

            yield return new object[] { "apiKey", validFilesSelfie };
            yield return new object[] { "apiKey", validFilesWithoutSelfie };
        }

    }
}
