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
	internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLoginModel>
	{
		public void Configure(EntityTypeBuilder<UserLoginModel> builder) =>
			builder.ToHistoryTable("UserLogin", SqlSchema.Identity, SqlSchema.History);
	}
}
