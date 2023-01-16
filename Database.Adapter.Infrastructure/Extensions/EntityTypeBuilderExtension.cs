using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Infrastructure.Extensions;

internal static class EntityTypeBuilderExtension
{
	/// <summary>
	/// Wraps the "ToTable" method from the EntityTypeBuilder extension.
	/// </summary>
	/// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder"/> itself.</param>
	/// <param name="tableName">The name of the table.</param>
	/// <param name="tableSchema">The schema of the table.</param>
	/// <param name="versionSchema">The schema of the versiong table schema.</param>
	/// <returns>The <see cref="EntityTypeBuilder"/> itself.</returns>
	public static EntityTypeBuilder ToSytemVersionedTable(this EntityTypeBuilder entityTypeBuilder, string tableName,
		string tableSchema = SqlSchema.PRIVATE, string versionSchema = SqlSchema.HISTORY) =>
		entityTypeBuilder.ToTable(tableName, tableSchema, e => e.IsTemporal(t => t.UseHistoryTable(tableName, versionSchema)));
}
