using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class AccountUserEntityConfiguration : AuditedCompositeConfiguration<AccountUserEntity>
{
	public override void Configure(EntityTypeBuilder<AccountUserEntity> builder)
	{
		builder.ToHistoryTable("AccountUser", SqlSchema.Finance, SqlSchema.History);

		builder.HasKey(e => new { e.AccountId, e.UserId })
			.IsClustered(false);

		base.Configure(builder);
	}
}
