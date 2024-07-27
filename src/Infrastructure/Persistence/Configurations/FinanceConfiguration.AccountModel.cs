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
	internal sealed class AccountModelConfiguration : AuditedConfiguration<AccountModel>
	{
		public override void Configure(EntityTypeBuilder<AccountModel> builder)
		{
			builder.ToHistoryTable("Account", SqlSchema.Finance, SqlSchema.History);

			builder.Property(e => e.IBAN)
				.IsUnicode(false);

			builder.HasIndex(e => e.IBAN)
				.IsClustered(false)
				.IsUnique(true);

			builder.HasMany(e => e.AccountUsers)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.AccountTransactions)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
