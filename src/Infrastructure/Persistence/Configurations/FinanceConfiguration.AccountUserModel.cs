using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountUserConfiguration : AuditedCompositeConfiguration<AccountUserModel>
	{
		public override void Configure(EntityTypeBuilder<AccountUserModel> builder)
		{
			builder.ToHistoryTable("AccountUser", SqlSchema.Finance, SqlSchema.History);

			builder.HasKey(e => new { e.AccountId, e.UserId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}
}
