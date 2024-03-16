using Domain.Interfaces.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The enumerator base configuration class.
/// </summary>
/// <typeparam name="T">
/// Must implement the <see cref="IEnumeratorModel"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class EnumeratorBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEnumeratorModel
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<T> builder)
	{
		builder.HasKey(x => x.Id)
			.IsClustered();

		builder.Property(e => e.Id)
			.IsRequired()
			.HasColumnOrder(1);

		builder.Property(e => e.Name)
			.HasMaxLength(128)
			.IsRequired()
			.HasColumnOrder(2);

		builder.Property(e => e.Description)
			.HasMaxLength(512)
			.IsRequired(false)
			.HasColumnOrder(3);

		builder.Property(e => e.IsDeleted)
			.HasDefaultValue(false)			
			.HasColumnOrder(4);

		builder.HasIndex(e => e.Name)
			.IsClustered()
			.IsUnique();
	}
}
