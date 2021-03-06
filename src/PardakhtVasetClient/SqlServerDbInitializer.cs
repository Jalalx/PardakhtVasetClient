﻿using System;

namespace Septa.PardakhtVaset.Client
{
    public class SqlServerDbInitializer : IDbInitializer
    {
        public string ConnectionString { get; }
        public IDbCommandExecutor DbCommandExecutor { get; }

        public SqlServerDbInitializer(string connectionString) : this(connectionString, new SqlDbCommandExecutor(connectionString))
        {
        }

        public SqlServerDbInitializer(string connectionString, IDbCommandExecutor dbCommandExecutor)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("message", nameof(connectionString));
            }

            ConnectionString = connectionString;
            DbCommandExecutor = dbCommandExecutor ?? throw new ArgumentNullException(nameof(dbCommandExecutor));
        }

        public void Init(string tablePrefix)
        {
            var script = @"
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = '$TABLEPREFIX$PaymentLinks'))
BEGIN
    CREATE TABLE [dbo].[$TABLEPREFIX$PaymentLinks](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Amount] [decimal](18, 2) NOT NULL,
    [FollowId] [nvarchar](50) NULL,
    [ClusterId] [nvarchar](50) NULL,
	[Token] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[CreateDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [LastCheckForUpdateDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [BankReferenceId] nvarchar(100) NULL,
    [ResultDate] [datetime] NULL,
	[ExpireDays] [int] NOT NULL DEFAULT 7,
	[Description] [nvarchar](max) NULL,
	[PaymentStatus] [int] NOT NULL DEFAULT 0
 );
END".Trim();

            tablePrefix = (tablePrefix ?? string.Empty).Trim();
            script = script.Replace("$TABLEPREFIX$", tablePrefix);

            DbCommandExecutor.Execute(script, null);
        }
    }
}
