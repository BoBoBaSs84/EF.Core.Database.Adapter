using Domain.Common.EntityBaseTypes.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The enumerator type base configuration class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity">
/// Must implement the <see cref="IEnumerator"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class EnumeratorTypeBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEnumerator
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(e => e.Id)
			.IsClustered(true);

		builder.HasIndex(e => e.Name)
			.IsClustered(false)
			.IsUnique(true);
	}
}
