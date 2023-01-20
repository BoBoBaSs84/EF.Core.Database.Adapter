using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Entities.Enumerators;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants.SqlSchema;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

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
