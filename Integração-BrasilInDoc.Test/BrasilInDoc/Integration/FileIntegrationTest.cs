//using Bogus;
//using Errors;
//using Integração_BrasilInDoc.BrasilInDoc.Infrastructure.Integration;
//using Shouldly;
//using System.Net;
//using System.Text;

//namespace Integração_BrasilInDoc.Test.BrasilInDoc.Integration
//{
//    [TestClass]
//    [TestCategory("File Integration Test")]
//    public class FileIntegrationTest
//    {
//        private readonly static Faker _faker = new("pt_BR");
//        private readonly string apiKey = "apiKeyTest";

//        [TestMethod]
//        [DynamicData(nameof(FileNotFoundErrorToSend))]
//        public async Task Upload_File_Async_FileNotFound(string filePathInvalid)
//        {
//            // Arrange
//            var receiver = _faker.Person.FirstName;

//            var service = new FileIntegration(new HttpClient());

//            // Act
//            var expectedResult = await Assert.ThrowsAsync<ErrorLists>(async () =>
//            {
//                await service.UploadFileAsync(apiKey, filePathInvalid, receiver);
//            });

//            // Assert
//            if (filePathInvalid == "" || expectedResult.Errors.Contains("Diretório"))
//                expectedResult.Errors.ShouldContain($"Diretório não encontrado ao tentar acessar o arquivo: {filePathInvalid}");

//            if (filePathInvalid is null)
//                expectedResult.Errors.ShouldContain("Caminho para o arquivo nulo");

//            if (expectedResult.Errors.Contains("Arquivo não encontrado"))
//                expectedResult.Errors.ShouldContain($"Arquivo não encontrado: {Path.GetFileName(filePathInvalid)}");

//        }
//        public static IEnumerable<object[]> FileNotFoundErrorToSend()
//        {
//            var filePathInvalid = Path.Combine("C:\\Arquivo\\", $"{Guid.NewGuid()}.txt");
//            var fileNotFound = Path.Combine("C:\\Users\\Ajinsoft\\Pictures\\imgs\\", $"{_faker.Name.FirstName()}.jpg");

//            yield return new object[] { "" };
//            yield return new object[] { null };
//            yield return new object[] { filePathInvalid };
//            yield return new object[] { fileNotFound };
//        }

//        //[TestMethod]
//        //[DynamicData(nameof(FileToSent))]
//        //public async Task Upload_File_Async_HttpException(string filePathValid, string receiver)
//        //{
//        //    // Arrange
//        //    var exception = new HttpRequestException("Falha na comunicação");
//        //    var requestMock = Helpes.Handlers.HttpHelperMock.CreateHttpClient
//        //        (out var handlerMock, ex: exception);

//        //    var service = new FileIntegration(requestMock);

//        //    // Act
//        //    var expectedResult = await Assert.ThrowsAsync<ErrorLists>(async () =>
//        //    {
//        //        await service.UploadFileAsync(apiKey, filePathValid, receiver);
//        //    });

//        //    // Assert
//        //    expectedResult.ShouldBeOfType<ErrorLists>();
//        //    expectedResult.Errors.ShouldContain($"Falha ao fazer upload do arquivo: {Path.GetFileName(filePathValid)}");
//        //}

//        //[TestMethod]
//        //[DynamicData(nameof(FileToSent))]
//        //public async Task Upload_File_Async_Sucess_Result(string filePathValid, string reciver)
//        //{
//        //    // Arrange
//        //    var expectedId = Guid.NewGuid();

//        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
//        //    {
//        //        Content = new StringContent("{\"id\":\"" + expectedId + "\"}", Encoding.UTF8, "application/json")
//        //    };
//        //    var responseMock = Helpes.Handlers.HttpHelperMock.CreateHttpClient
//        //        (out var handlerMock, response: response);

//        //    var service = new FileIntegration(responseMock);

//        //    // Act
//        //    var expectedResult = await service.UploadFileAsync(apiKey, filePathValid, reciver);

//        //    // Assert
//        //    expectedResult.ShouldBeOfType<string>();
//        //    expectedResult.ShouldBe(expectedId.ToString());
//        //}

//        //public static IEnumerable<object[]> FileToSent()
//        //{
//        //    var filePathValid = "C:\\Users\\Ajinsoft\\Pictures\\imgs\\identity_test.jpg";
//        //    var receiver = _faker.Person.FirstName;

//        //    yield return new object[] { filePathValid, receiver };
//        //}
//    }
//}
