using Moq;
using Septa.PardakhtVaset.Client;
using System;
using Xunit;

namespace PardakhtVasetClientTests
{
    public class SqlServerDbInitializerTests
    {
        [Fact]
        public void CreateInstance_UsingNullConnectionString_ThrowsArgumentException()
        {
            var ex = Record.Exception(() => new SqlServerDbInitializer(null));
            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public void CreateInstance_UsingEmptyConnectionString_ThrowsArgumentException()
        {
            var ex = Record.Exception(() => new SqlServerDbInitializer(""));
            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public void Init_UsingValidSchemaNameAndNullTablePrefix_ReplaceScriptCorrectly()
        {
            var expectedScript = @"
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'PaymentLinks'))
BEGIN
    CREATE TABLE [dbo].[PaymentLinks](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Amount] [decimal](18, 2) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[CreateDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[ExpireDays] [int] NOT NULL DEFAULT 7,
	[Description] [nvarchar](max) NULL,
	[PaymentStatus] [int] NOT NULL DEFAULT 0
 );
END".Trim();

            var mockedCommandExecutor = new Mock<IDbCommandExecutor>();
            mockedCommandExecutor.Setup(x => x.Execute(expectedScript, null)).Verifiable();

            var dbInitializer = new SqlServerDbInitializer("sample connection string", mockedCommandExecutor.Object);
            

            dbInitializer.Init(null);

            mockedCommandExecutor.Verify(x => x.Execute(expectedScript, null), Times.Once());
        }
    }
}
