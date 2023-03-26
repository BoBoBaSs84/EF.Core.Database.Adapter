using Domain.Common.EntityBaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The enumerator type base configuration class.
/// </summary>
/// <typeparam name="TEntity">
/// Must implement the <see cref="IEnumerator"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class EnumeratorTypeBaseConfiguration<TEntity> : EntityTypeBaseConfiguration<TEntity> where TEntity : class, IEnumerator
{
	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(e => e.Id)
			.IsClustered(true);

		builder.HasIndex(e => e.Name)
			.IsClustered(false)
			.IsUnique(true);

		builder.HasQueryFilter(x => x.IsActive.Equals(true));

		Configure(builder, SqlSchema.ENUMERATOR);
	}
}
