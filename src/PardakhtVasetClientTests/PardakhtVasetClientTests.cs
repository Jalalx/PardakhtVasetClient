using Moq;
using PardakhtVasetServices;
using Septa.PardakhtVaset.Client;
using System;
using Xunit;

namespace PardakhtVasetClientTests
{
    public class PardakhtVasetClientTests
    {
        [Fact]
        public void CreateInstance_WithNullParameterForOptions_ThrowsArgumentNullException()
        {
            var ex = Record.Exception(() => new PardakhtVasetClient(null));
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void CreateInstance_WithNullParameterForOptionsAndDbInitializer_ThrowsArgumentNullException()
        {
            var ex = Record.Exception(() => new PardakhtVasetClient(null, null, null, null));
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void CreateInstance_WithCorrectOptions_CreatesNewInstance()
        {
            var options = new PardakhtVasetClientOptions { ApiKey = "my api key", ConnectionString = "my connection string;", TablePrefix = "pv" };
            var client = new PardakhtVasetClient(options);

            Assert.NotNull(client);
            Assert.Equal(options.ConnectionString, client.Options.ConnectionString);
            Assert.Equal(options.ApiKey, client.Options.ApiKey);
            Assert.Equal(options.TablePrefix, client.Options.TablePrefix);
        }

        [Fact]
        public void Dispose_WithExistingSubscribers_DisposeWithoutError()
        {
            var options = new PardakhtVasetClientOptions { ApiKey = "my api key", ConnectionString = "my connection string;", TablePrefix = "pv" };

            var mockedDbInitializer = new Mock<IDbInitializer>();
            var mockedPaymentRepository = new Mock<IPaymentLinkRepository>();
            var mockedPaymetRequestFactory = new Mock<IPayRequestFactory>();
            var mockedPayRequest = new Mock<IPayRequest>();

            mockedPaymetRequestFactory.Setup(x => x.Create()).Returns(mockedPayRequest.Object);

            var client = new PardakhtVasetClient(options, mockedDbInitializer.Object, mockedPaymentRepository.Object, mockedPaymetRequestFactory.Object);

            client.PaymentLinkNotificationService.PaymentLinkChanged += (o, e) => { };
            var ex = Record.Exception(() => client.Dispose());

            Assert.Null(ex);
            Assert.NotNull(client);
            mockedPaymentRepository.Verify(x => x.Dispose(), Times.Once);

        }
    }
}
