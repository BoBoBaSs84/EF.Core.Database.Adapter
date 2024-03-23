using Domain.Interfaces.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The identity type base configuration class.
/// </summary>
/// <typeparam name="T">
/// Must implement the <see cref="IIdentityModel"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class IdentityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IIdentityModel, IConcurrencyModel
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<T> builder)
	{
		builder.HasKey(e => e.Id)
			.IsClustered(false);

		builder.Property(e => e.Id)
			.HasDefaultValueSql("NEWID()")
			.ValueGeneratedOnAdd()
			.HasColumnOrder(1);

		builder.Property(e => e.Timestamp)
			.IsRowVersion()
			.HasColumnOrder(2);
	}
}
