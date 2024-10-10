using Infrastructure.Common;
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
	/// Applies the create database logging operation to migration builder.
	/// </summary>
	/// <param name="migrationBuilder">The migration builder to use.</param>
	/// <param name="schemaName">The name of the table schema.</param>
	/// <param name="tableName">The name of the table.</param>
	/// <returns><see cref="OperationBuilder{TOperation}"/></returns>
	internal static OperationBuilder<CreateDatabaseLogOperation> AddDatabaseEventLog(this MigrationBuilder migrationBuilder, string? schemaName = null, string? tableName = null)
	{
		schemaName ??= InfrastructureConstants.SqlSchema.Private;
		tableName ??= "DatabaseEventLog";

		CreateDatabaseLogOperation operation = new(schemaName, tableName);

		migrationBuilder.Operations.Add(operation);

		return new OperationBuilder<CreateDatabaseLogOperation>(operation);
	}
}
