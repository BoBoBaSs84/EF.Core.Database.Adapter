﻿using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Todo;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class TodoConfiguration
{
	/// <inheritdoc/>
	internal sealed class TodoItemModelConfiguration : AuditedConfiguration<ItemModel>
	{
		public override void Configure(EntityTypeBuilder<ItemModel> builder)
		{
			builder.ToHistoryTable("Item", SqlSchema.Todo, SqlSchema.History);

			builder.Property(e => e.Title)
				.HasMaxLength(256);

			builder.Property(e => e.Note)
				.HasMaxLength(2048);

			base.Configure(builder);
		}
	}
}
