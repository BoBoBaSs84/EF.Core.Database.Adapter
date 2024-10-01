using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Common.Constants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal class UserTokenConfiguration : IEntityTypeConfiguration<UserTokenModel>
	{
		public void Configure(EntityTypeBuilder<UserTokenModel> builder) =>
			builder.ToHistoryTable("UserToken", SqlSchema.Identity, SqlSchema.History);
	}
}
