using Domain.Entities.Identity;
using Domain.Enumerators;
using Domain.Extensions;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal sealed class RoleConfiguration : IEntityTypeConfiguration<RoleModel>
	{
		public void Configure(EntityTypeBuilder<RoleModel> builder)
		{
			builder.ToSytemVersionedTable(SqlSchema.Identity, "Role");

			builder.HasMany(e => e.UserRoles)
				.WithOne(e => e.Role)
				.HasForeignKey(ur => ur.RoleId)
				.IsRequired();

			builder.HasMany(e => e.RoleClaims)
				.WithOne(e => e.Role)
				.HasForeignKey(rc => rc.RoleId)
				.IsRequired();

			builder.HasData(GetRoleTypes());
		}

		private static ICollection<RoleModel> GetRoleTypes()
		{
			List<RoleType> roleTypes = RoleType.ADMINISTRATOR.GetListFromEnum();
			ICollection<RoleModel> listToReturn = new List<RoleModel>();

			foreach (RoleType roleType in roleTypes)
				listToReturn.Add(new RoleModel()
				{
					Id = Guid.NewGuid(),
					Name = roleType.GetName(),
					NormalizedName = roleType.ToString(),
					Description = roleType.GetDescription()
				});
			return listToReturn;
		}
	}
}
