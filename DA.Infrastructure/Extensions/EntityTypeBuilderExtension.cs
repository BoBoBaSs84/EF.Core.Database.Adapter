using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static DA.Models.Constants.Sql;

namespace DA.Infrastructure.Extensions;

internal static class EntityTypeBuilderExtension
{
	/// <summary>
	/// Wraps the "ToTable" method from the EntityTypeBuilder extension.
	/// </summary>
	/// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder"/> itself.</param>
	/// <param name="tableName">The name of the table.</param>
	/// <param name="tableSchema">The schema of the table.</param>
	/// <param name="versionSchema">The schema of the versiong table schema.</param>
	public static void ToSytemVersionedTable(
		this EntityTypeBuilder entityTypeBuilder,
		string tableName,
		string tableSchema = Schema.PRIVATE,
		string versionSchema = Schema.HISTORY
		) =>
		entityTypeBuilder.ToTable(
			tableName, tableSchema, e => e.IsTemporal(
				t => t.UseHistoryTable(tableName, versionSchema)));
}
