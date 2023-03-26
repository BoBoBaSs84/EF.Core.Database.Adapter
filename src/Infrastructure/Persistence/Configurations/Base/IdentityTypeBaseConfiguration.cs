using Domain.Common.EntityBaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The identity type base configuration class.
/// </summary>
/// <typeparam name="TEntity">
/// Must implement the <see cref="IIdentity{T}"/> and <see cref="IConcurrency"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class IdentityTypeBaseConfiguration<TEntity> : EntityTypeBaseConfiguration<TEntity> where TEntity : class, IIdentity<int>, IConcurrency
{
	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<TEntity> builder, string tableSchema)
	{
		builder.HasKey(e => e.Id)
			.IsClustered(false);

		base.Configure(builder, tableSchema);
	}

	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(e => e.Id)
			.IsClustered(false);

		base.Configure(builder, SqlSchema.PRIVATE);
	}
}
