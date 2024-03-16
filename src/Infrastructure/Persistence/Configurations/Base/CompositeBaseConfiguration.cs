using Domain.Interfaces.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The composite type base configuration class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="T"></typeparam>
internal abstract class CompositeBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IConcurrencyModel, IAuditedModel
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<T> builder)
	{
		builder.Property(e => e.Timestamp)
			.IsRowVersion()
			.HasColumnOrder(1);

		builder.Property(e => e.CreatedBy)
			.IsRequired()
			.HasColumnOrder(2);

		builder.Property(e => e.ModifiedBy)
			.IsRequired(false)
			.HasColumnOrder(3);
	}
}
