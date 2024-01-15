using Infrastructure.Persistence.Generators.Operations;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;

namespace Infrastructure.Persistence.Generators;


/// <summary>
/// The repository sql generator class.
/// </summary>
/// <inheritdoc/>
internal sealed class RepositorySqlGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer) : SqlServerMigrationsSqlGenerator(dependencies, commandBatchPreparer)
{
	protected override void Generate(MigrationOperation operation, IModel? model, MigrationCommandListBuilder builder)
	{
		if (operation is CreateDatabaseLogOperation logOperation)
		{
			Generate(logOperation, builder);
		}
		else
		{
			base.Generate(operation, model, builder);
		}
	}

	private void Generate(CreateDatabaseLogOperation operation, MigrationCommandListBuilder builder)
	{
		var sqlHelper = Dependencies.SqlGenerationHelper;

		builder.Append("IF SCHEMA_ID(N'");
		builder.Append(operation.SchemaName);
		builder.Append("') IS NULL ");
		builder.Append("EXEC(N'CREATE SCHEMA ");
		builder.Append(sqlHelper.DelimitIdentifier(operation.SchemaName));
		builder.Append(";')");
		builder.Append(sqlHelper.StatementTerminator);
		builder.AppendLine();
		builder.EndCommand();
		builder.AppendLine();
		builder.Append("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'");
		builder.Append(sqlHelper.DelimitIdentifier(operation.SchemaName));
		builder.Append(".");
		builder.Append(sqlHelper.DelimitIdentifier(operation.TableName));
		builder.Append("') AND type in (N'U')) DROP TABLE ");
		builder.Append(sqlHelper.DelimitIdentifier(operation.SchemaName));
		builder.Append(".");
		builder.Append(sqlHelper.DelimitIdentifier(operation.TableName));
		builder.Append(sqlHelper.StatementTerminator);
		builder.AppendLine();
		builder.EndCommand();
		builder.AppendLine();
		builder.Append("CREATE TABLE ");
		builder.Append(sqlHelper.DelimitIdentifier(operation.SchemaName));
		builder.Append(".");
		builder.Append(sqlHelper.DelimitIdentifier(operation.TableName));
		builder.Append("(");
		builder.AppendLine();
		builder.Append("[Id] [uniqueidentifier] NOT NULL CONSTRAINT [PK_");
		builder.Append(operation.TableName);
		builder.Append("] PRIMARY KEY NONCLUSTERED,");
		builder.AppendLine();
		builder.Append("[PostTime] [datetime] NOT NULL CONSTRAINT [DF_");
		builder.Append(operation.TableName);
		builder.Append("_PostTime] DEFAULT (getdate()),");
		builder.AppendLine();
		builder.Append("[Login] [sysname] NOT NULL CONSTRAINT [DF_");
		builder.Append(operation.TableName);
		builder.Append("_Login] DEFAULT (original_login()),");
		builder.AppendLine();
		builder.Append("[Application] [sysname] NOT NULL CONSTRAINT [DF_");
		builder.Append(operation.TableName);
		builder.Append("_Application] DEFAULT (program_name()),");
		builder.AppendLine();
		builder.AppendLine("[Event] [sysname] NULL,");
		builder.AppendLine("[Schema] [sysname] NULL,");
		builder.AppendLine("[Object] [sysname] NULL,");
		builder.AppendLine("[TSQL] [nvarchar](max) NULL,");
		builder.AppendLine("[XmlEvent] [xml] NOT NULL");
		builder.Append(")");
		builder.Append(sqlHelper.StatementTerminator);
		builder.AppendLine();
		builder.EndCommand();
		builder.AppendLine();
		builder.AppendLines(@"CREATE OR ALTER TRIGGER [DatabaseTriggerLog] ON DATABASE 
FOR DDL_DATABASE_LEVEL_EVENTS AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @data XML,
		@schema sysname,
		@object sysname,
		@eventType sysname;

	SET @data = EVENTDATA();
	SET @eventType = @data.value('(/EVENT_INSTANCE/EventType)[1]', 'sysname');
	SET @schema = @data.value('(/EVENT_INSTANCE/SchemaName)[1]', 'sysname');
	SET @object = @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 'sysname');");
		builder.AppendLine();
		builder.Append("INSERT ");
		builder.Append(sqlHelper.DelimitIdentifier(operation.SchemaName));
		builder.Append(".");
		builder.Append(sqlHelper.DelimitIdentifier(operation.TableName));
		builder.Append("([Id], [Event], [Schema], [Object], [TSQL], [XmlEvent])");
		builder.AppendLines(@"SELECT NEWID()
		, @eventType
		, CONVERT(sysname, @schema)
		, CONVERT(sysname, @object)
		, @data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 'nvarchar(max)')
		, @data;
END");
		builder.Append(sqlHelper.StatementTerminator);
		builder.AppendLine();
		builder.EndCommand();
		builder.AppendLine();
	}
}
