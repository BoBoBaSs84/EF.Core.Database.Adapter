﻿using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class FinanceConfiguration
{
	/// <inheritdoc/>
	internal sealed class CardConfiguration : AuditedConfiguration<CardModel>
	{
		public override void Configure(EntityTypeBuilder<CardModel> builder)
		{
			builder.ToHistoryTable("Card", SqlSchema.Finance, SqlSchema.History);

			builder.HasIndex(i => i.PAN)
				.IsClustered(false)
				.IsUnique(true);

			builder.Property(p => p.PAN)
				.HasMaxLength(SqlMaxLength.MAX_25)
				.IsUnicode(false);

			builder.Property(p => p.ValidUntil)
				.HasColumnType(SqlDataType.DATE);

			builder.HasMany(e => e.Transactions)
				.WithOne(e => e.Card)
				.HasForeignKey(e => e.CardId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
