using Domain.Models.Finance;

using Infrastructure.Extensions;
using Infrastructure.Persistence.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountUserConfiguration : CompositeBaseConfiguration<AccountUserModel>
	{
		public override void Configure(EntityTypeBuilder<AccountUserModel> builder)
		{
			builder.ToVersionedTable(SqlSchema.Finance, "AccountUser");

			builder.HasKey(e => new { e.AccountId, e.UserId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}
}
