using DA.Infrastructure.Extensions;
using DA.Models.Enumerators;
using DA.Models.Extensions;
using DA.Models.Contexts.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Models.Constants.Sql.Schema;

namespace DA.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToSytemVersionedTable(nameof(Role), IDENTITY);

		builder.HasMany(e => e.UserRoles)
			.WithOne(e => e.Role)
			.HasForeignKey(ur => ur.RoleId)
			.IsRequired(true);

		builder.HasMany(e => e.RoleClaims)
			.WithOne(e => e.Role)
			.HasForeignKey(rc => rc.RoleId)
			.IsRequired(true);

		builder.HasData(GetRoleTypes());
	}

	private static ICollection<Role> GetRoleTypes()
	{
		List<RoleType> roleTypes = RoleType.ADMINISTRATOR.GetListFromEnum();
		ICollection<Role> listToReturn = new List<Role>();

		foreach (RoleType roleType in roleTypes)
			listToReturn.Add(new Role()
			{
				Id = (int)roleType,
				Name = roleType.GetName(),
				NormalizedName = roleType.ToString(),
				Description = roleType.GetDescription()
			});
		return listToReturn;
	}
}
