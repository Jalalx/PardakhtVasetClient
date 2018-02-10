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
            var expectedColumns = new[]
            {
                nameof(PaymentLink.Id),
                nameof(PaymentLink.Amount),
                nameof(PaymentLink.BankReferenceId),
                nameof(PaymentLink.CreateDate),
                nameof(PaymentLink.Description),
                nameof(PaymentLink.ExpireDays),
                nameof(PaymentLink.FollowId),
                nameof(PaymentLink.LastCheckForUpdateDate),
                nameof(PaymentLink.PaymentStatus),
                nameof(PaymentLink.ResultDate),
                nameof(PaymentLink.Token),
                nameof(PaymentLink.Url)
            };

            var options = new PardakhtVasetClientOptions();
            options.ConnectionString = ConnectionStrings;
            options.TablePrefix = tablePrefix;

            var client = new PardakhtVasetClient(options);

            client.Init();

            Assert.True(TableExists(expectedTableName));
            Assert.True(ColumnsExists(expectedTableName, expectedColumns));
        }

        // cleanup
        public override void Dispose()
        {

        }
    }
}
