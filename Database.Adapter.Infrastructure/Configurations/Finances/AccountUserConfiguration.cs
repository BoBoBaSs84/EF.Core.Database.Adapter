using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants.SqlSchema;

namespace Database.Adapter.Infrastructure.Configurations.Finances;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
{
	public void Configure(EntityTypeBuilder<AccountUser> builder)
	{
		builder.ToSytemVersionedTable(nameof(AccountUser), FINANCE);

		builder.HasKey(e => new { e.AccountId, e.UserId })
			.IsClustered(false);
	}
}
