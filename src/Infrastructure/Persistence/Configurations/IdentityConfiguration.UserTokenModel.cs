using Domain.Models.Identity;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal class UserTokenConfiguration : IEntityTypeConfiguration<UserTokenModel>
	{
		public void Configure(EntityTypeBuilder<UserTokenModel> builder) =>
			builder.ToVersionedTable(SqlSchema.Identity, "UserToken");
	}
}
