﻿using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class AccountTransactionConfiguration : AuditedCompositeConfiguration<AccountTransactionModel>
	{
		public override void Configure(EntityTypeBuilder<AccountTransactionModel> builder)
		{
			builder.ToHistoryTable("AccountTransaction", SqlSchema.Finance, SqlSchema.History);

			builder.HasKey(e => new { e.AccountId, e.TransactionId })
				.IsClustered(false);

			base.Configure(builder);
		}
	}
}
