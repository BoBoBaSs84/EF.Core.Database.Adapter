using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlMaxLength = Domain.Common.Constants.Sql.MaxLength;
using SqlSchema = Domain.Common.Constants.Sql.Schema;

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

			builder.HasIndex(i => i.IBAN)
				.IsClustered(false)
				.IsUnique(true);

			builder.Property(p => p.IBAN)
				.HasMaxLength(SqlMaxLength.MAX_25)
				.IsUnicode(false);

			builder.Property(p => p.Provider)
				.HasMaxLength(SqlMaxLength.MAX_500);

			builder.HasMany(e => e.AccountUsers)
				.WithOne(e => e.Account)
				.HasForeignKey(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Transactions)
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
