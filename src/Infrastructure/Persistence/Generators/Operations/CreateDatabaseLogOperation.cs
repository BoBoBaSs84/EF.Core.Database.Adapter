using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Infrastructure.Persistence.Generators.Operations;

/// <summary>
/// The create database log operation class.
/// </summary>
internal class CreateDatabaseLogOperation : MigrationOperation
{
	/// <summary>
	/// Initializes a new instance of the create database log operation class.
	/// </summary>
	/// <param name="schemaName">The name of the table schema.</param>
	/// <param name="tableName">The name of the table.</param>
	public CreateDatabaseLogOperation(string schemaName, string tableName)
	{
		SchemaName = schemaName;
		TableName = tableName;
	}

	/// <summary>
	/// The name of the table schema.
	/// </summary>
	public string SchemaName { get; }

	/// <summary>
	/// The name of the table.
	/// </summary>
	public string TableName { get; }
}
