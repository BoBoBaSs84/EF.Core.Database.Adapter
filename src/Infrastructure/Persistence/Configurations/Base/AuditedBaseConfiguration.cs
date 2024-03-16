using Domain.Interfaces.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// The audited base configuration class.
/// </summary>
/// <typeparam name="T">
/// Must implement the <see cref="IAuditedModel"/> interface.
/// </typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal abstract class AuditedBaseConfiguration<T> : IdentityBaseConfiguration<T> where T : class, IIdentityModel, IConcurrencyModel, IAuditedModel
{
	/// <inheritdoc/>
	public override void Configure(EntityTypeBuilder<T> builder)
	{
		builder.Property(e => e.CreatedBy)
			.IsRequired()
			.HasColumnOrder(3);

		builder.Property(e => e.ModifiedBy)
			.IsRequired(false)
			.HasColumnOrder(4);

		base.Configure(builder);
	}
}
