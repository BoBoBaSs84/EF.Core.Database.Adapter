using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BB84.Home.Infrastructure.Persistence.Generators.Operations;

/// <summary>
/// The create database log operation class.
/// </summary>
/// <param name="schemaName">The name of the table schema.</param>
/// <param name="tableName">The name of the table.</param>
internal class CreateDatabaseLogOperation(string schemaName, string tableName) : MigrationOperation
{
	/// <summary>
	/// The name of the table schema.
	/// </summary>
	public string SchemaName { get; } = schemaName;

	/// <summary>
	/// The name of the table.
	/// </summary>
	public string TableName { get; } = tableName;
}
