using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Schema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Extensions;

/// <summary>
/// The entity type builder extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class EntityTypeBuilderExtensions
{
	/// <summary>
	/// Wraps the "ToTable" method from the EntityTypeBuilder extension.
	/// </summary>
	/// <param name="entityTypeBuilder">The <see cref="EntityTypeBuilder"/> to work with.</param>
	/// <param name="tableName">The name of the table.</param>
	/// <param name="tableSchema">The schema of the table.</param>
	/// <param name="versionSchema">The schema of the versiong table schema.</param>
	/// <returns>The enriched <paramref name="entityTypeBuilder"/> class.</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static EntityTypeBuilder ToSytemVersionedTable(this EntityTypeBuilder entityTypeBuilder,
		string tableName, string? tableSchema = Schema.PRIVATE, string? versionSchema = Schema.HISTORY)
	{
		if (string.IsNullOrWhiteSpace(tableName))
			throw new ArgumentNullException(nameof(tableName));

		entityTypeBuilder.ToTable(tableName, tableSchema, e => e.IsTemporal(t => t.UseHistoryTable(tableName, versionSchema)));

		return entityTypeBuilder;
	}
}
