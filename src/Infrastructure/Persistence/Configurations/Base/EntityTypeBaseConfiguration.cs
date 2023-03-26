using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The entity type base configuration class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity"></typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class EntityTypeBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
	/// <summary>
	/// Configures the entity of type <typeparamref name="TEntity" />.
	/// </summary>
	/// <param name="builder">The builder to be used to configure the entity type.</param>
	/// <param name="tableSchema">The database table schema.</param>
	public virtual void Configure(EntityTypeBuilder<TEntity> builder, string tableSchema)
	{
		builder.ToSytemVersionedTable(nameof(TEntity), tableSchema);
		
		Configure(builder);
	}

	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
	}
}
