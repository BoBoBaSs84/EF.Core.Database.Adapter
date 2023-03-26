using Domain.Common.EntityBaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The identity type base configuration class.
/// </summary>
/// <typeparam name="TEntity">
/// Must implement the <see cref="IIdentity{T}"/> and <see cref="IConcurrency"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class IdentityTypeBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IIdentity<int>, IConcurrency
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.Property(e => e.Id)
			.IsRequired(true);

		builder.HasKey(e => e.Id)
			.IsClustered(false);
	}
}
