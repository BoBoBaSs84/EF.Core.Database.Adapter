using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;
using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Documents;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Documents;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal sealed class DocumentUserConfiguration : AuditedCompositeConfiguration<DocumentUser>
{
	public override void Configure(EntityTypeBuilder<DocumentUser> builder)
	{
		builder.ToHistoryTable(nameof(DocumentUser), SqlSchema.Documents, SqlSchema.History);

		builder.HasKey(k => new { k.DocumentId, k.UserId })
			.IsClustered(false);

		builder.HasOne(e => e.Document)
			.WithMany(e => e.DocumentUsers)
			.HasForeignKey(e => e.DocumentId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		builder.HasOne(e => e.User)
			.WithMany(e => e.DocumentUsers)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();

		base.Configure(builder);
	}
}
