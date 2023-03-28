using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The composite type base configuration class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity"></typeparam>
internal abstract class CompositeTypeBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
	}
}
