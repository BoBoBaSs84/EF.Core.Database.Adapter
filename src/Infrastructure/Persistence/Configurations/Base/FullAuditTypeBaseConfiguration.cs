using Domain.Common.EntityBaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The full audit type base configuration class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityTypeBaseConfiguration{TEntity}"/> class.
/// </remarks>
/// <typeparam name="TEntity">
/// Must implement the <see cref="IFullAudited"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, E.")]
internal abstract class FullAuditTypeBaseConfiguration<TEntity> : IdentityTypeBaseConfiguration<TEntity> where TEntity : class, IFullAudited
{
	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<TEntity> builder, string tableSchema)
	{
		builder.HasQueryFilter(x => x.IsDeleted.Equals(false));

		base.Configure(builder, tableSchema);
	}

	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasQueryFilter(x => x.IsDeleted.Equals(false));

		base.Configure(builder);
	}
}
