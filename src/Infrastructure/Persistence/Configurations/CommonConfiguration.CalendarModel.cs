using BB84.EntityFrameworkCore.Repositories.SqlServer.Configurations;

using Domain.Models.Common;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, entity type configuration.")]
internal static partial class CommonConfiguration
{
	/// <inheritdoc/>
	internal sealed class CalendarConfiguration : IdentityConfiguration<CalendarModel>
	{
		/// <inheritdoc/>
		public override void Configure(EntityTypeBuilder<CalendarModel> builder)
		{
			builder.ToVersionedTable(SqlSchema.Common, "Calendar");

			builder.HasIndex(e => e.Date)
				.IsUnique(true);

			builder.HasMany(e => e.Attendances)
				.WithOne(e => e.Calendar)
				.HasForeignKey(e => e.CalendarId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
