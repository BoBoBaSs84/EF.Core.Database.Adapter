using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Domain.Constants.Sql.Schema;

namespace Infrastructure.Extensions;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class EntityTypeBuilderExtension
{
	/// <summary>
	/// Wraps the "ToTable" method from the EntityTypeBuilder extension.
	/// </summary>
	/// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder"/> itself.</param>
	/// <param name="tableName">The name of the table.</param>
	/// <param name="tableSchema">The schema of the table.</param>
	/// <param name="versionSchema">The schema of the versiong table schema.</param>
	public static EntityTypeBuilder ToSytemVersionedTable(this EntityTypeBuilder entityTypeBuilder,
		string tableName, string? tableSchema = PRIVATE, string? versionSchema = HISTORY)
	{
		if (string.IsNullOrWhiteSpace(tableName))
			throw new ArgumentNullException(nameof(tableName));

		entityTypeBuilder.ToTable(tableName, tableSchema, e => e.IsTemporal(t => t.UseHistoryTable(tableName, versionSchema)));

		return entityTypeBuilder;
	}
}
