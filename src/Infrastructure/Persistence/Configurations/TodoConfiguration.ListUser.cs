using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Todo;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class TodoConfiguration
{
	/// <inheritdoc/>
	internal sealed class TodoListUserConfiguration : AuditedCompositeConfiguration<ListUser>
	{
		public override void Configure(EntityTypeBuilder<ListUser> builder)
		{
			builder.ToHistoryTable("ListUser", SqlSchema.Todo, SqlSchema.History);

			builder.HasKey(k => new { k.ListId, k.UserId })
				.IsClustered(false);

			builder.HasOne(e => e.User)
				.WithMany(e => e.TodoLists)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasOne(e => e.TodoList)
				.WithMany(e => e.Users)
				.HasForeignKey(e => e.ListId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
