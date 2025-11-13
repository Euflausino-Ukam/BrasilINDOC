using Moq;
using Moq.Protected;

namespace Integração_BrasilInDoc.Test.Helpes.Handlers
{
    public class HttpHelperMock
    {
        public static HttpClient CreateHttpClient
            (
                out Mock<HttpMessageHandler> handlerMock,
                HttpResponseMessage? response = null,
                Exception? ex = null
            )
        {
            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            var setup = handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                );

            if (ex != null) setup.ThrowsAsync(ex);
            else if (response != null) setup.ReturnsAsync(response);

            var client = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://app-h.brasilindoc.com.br")
            };
            return client;
        }

    }
}
