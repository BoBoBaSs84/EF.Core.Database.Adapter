using Domain.Entities.Identity;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleModel>
	{
		public void Configure(EntityTypeBuilder<UserRoleModel> builder) =>
			builder.ToVersionedTable(SqlSchema.Identity, "UserRole");
	}
}
