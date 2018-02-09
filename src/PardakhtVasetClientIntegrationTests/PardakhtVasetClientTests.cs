using Septa.PardakhtVaset.Client;
using Xunit;

namespace PardakhtVasetClientIntegrationTests
{
    public class PardakhtVasetClientTests : DatabaseBaseTest
    {
        // initialize
        public PardakhtVasetClientTests()
        {

        }

        [AutoRollback]
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("pv")]
        public void Init_UsingValidOptions_CreatesTable(string tablePrefix)
        {
            var expectedTableName = tablePrefix + "PaymentLinks";

            var options = new PardakhtVasetClientOptions();
            options.ConnectionString = ConnectionStrings;
            options.TablePrefix = tablePrefix;

            var client = new PardakhtVasetClient(options);

            client.Init();

            Assert.True(TableExists(expectedTableName));
        }

        // cleanup
        public override void Dispose()
        {

        }
    }
}
