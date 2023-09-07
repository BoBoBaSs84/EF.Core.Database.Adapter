using Infrastructure.Persistence.Generators.Operations;

using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace Infrastructure.Extensions;

/// <summary>
/// The migration builder extensions class.
/// </summary>
internal static class MigrationBuilderExtensions
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="migrationBuilder">The migration builder to use.</param>
	/// <param name="schemaName">The name of the table schema.</param>
	/// <param name="tableName">The name of the table.</param>
	/// <returns><see cref="OperationBuilder{TOperation}"/></returns>
	internal static OperationBuilder<CreateDatabaseLogOperation> CreateDatabaseLog(this MigrationBuilder migrationBuilder, string schemaName = "Migration", string tableName = "DatabaseLog")
	{
		CreateDatabaseLogOperation operation = new(schemaName, tableName);

		migrationBuilder.Operations.Add(operation);

		return new OperationBuilder<CreateDatabaseLogOperation>(operation);
	}
}
