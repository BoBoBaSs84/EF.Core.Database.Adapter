using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Extensions;

/// <summary>
/// The entity type builder extensions class.
/// </summary>
internal static class EntityTypeBuilderExtensions
{
	/// <summary>
	/// Wraps the "ToTable" method from the EntityTypeBuilder extension.
	/// </summary>
	/// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder"/> to work with.</param>
	/// <param name="tableSchema">The database schema of the table.</param>
	/// <param name="tableName">The name of the database table.</param>
	/// <param name="versionSchema">The database schema of the versioning table.</param>
	/// <returns>The enriched <paramref name="entityTypeBuilder"/> class.</returns>
	public static EntityTypeBuilder ToSytemVersionedTable(this EntityTypeBuilder entityTypeBuilder,
		string? tableSchema = SqlSchema.PRIVATE, string? tableName = null, string? versionSchema = SqlSchema.HISTORY)
	{
		tableName ??= entityTypeBuilder.Metadata.ClrType.Name;

		return entityTypeBuilder.ToTable(
			tableName, tableSchema, e => e.IsTemporal(
				t => t.UseHistoryTable(tableName, versionSchema)));
	}
}
