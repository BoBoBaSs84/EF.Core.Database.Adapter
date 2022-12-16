using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Adapter.Infrastructure.Configurations.MasterData;

/// <inheritdoc/>
internal sealed class EnDayTypeConfiguration : IEntityTypeConfiguration<EnDayType>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<EnDayType> builder) =>
		builder.ToSytemVersionedTable(nameof(EnDayType));
}
