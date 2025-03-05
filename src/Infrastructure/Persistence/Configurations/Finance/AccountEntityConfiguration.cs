using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Finance;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class AccountEntityConfiguration : AuditedConfiguration<AccountEntity>
{
	public override void Configure(EntityTypeBuilder<AccountEntity> builder)
	{
		builder.ToHistoryTable("Account", SqlSchema.Finance, SqlSchema.History);

		builder.HasIndex(i => i.IBAN)
			.IsClustered(false)
			.IsUnique(true);

		builder.Property(p => p.IBAN)
			.HasMaxLength(25)
			.IsUnicode(false);

		builder.Property(p => p.Provider)
			.HasMaxLength(500);

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
