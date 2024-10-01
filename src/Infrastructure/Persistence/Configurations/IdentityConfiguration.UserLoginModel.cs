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
	internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLoginModel>
	{
		public void Configure(EntityTypeBuilder<UserLoginModel> builder) =>
			builder.ToHistoryTable("UserLogin", SqlSchema.Identity, SqlSchema.History);
	}
}
