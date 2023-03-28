using Domain.Common.EntityBaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The identity type base configuration class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity">
/// Must implement the <see cref="IIdentity{T}"/> and <see cref="IConcurrency"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class IdentityTypeBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IIdentity<int>, IConcurrency
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.Property(e => e.Id)
			.UseIdentityColumn();

		builder.HasKey(e => e.Id)
			.IsClustered(false);
	}
}
