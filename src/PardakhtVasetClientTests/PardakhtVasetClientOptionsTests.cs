using Septa.PardakhtVaset.Client;
using Xunit;

namespace PardakhtVasetClientTests
{
    public class PardakhtVasetClientOptionsTests
    {
        [Fact]
        public void CreateInstance_WithNoParameterOrInitialization_HasExpectedDefaultValues()
        {
            var options = new PardakhtVasetClientOptions();

            Assert.Null(options.ApiKey);
            Assert.Null(options.ConnectionString);
            Assert.Equal("", options.TablePrefix);
        }
    }
}
