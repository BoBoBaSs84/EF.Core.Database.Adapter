using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

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
