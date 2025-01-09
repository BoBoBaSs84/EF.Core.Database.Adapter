using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Extensions;

using Domain.Enumerators;
using Domain.Extensions;
using Domain.Models.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="RoleModel"/> entity.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class RoleConfiguration : IEntityTypeConfiguration<RoleModel>
{
	public void Configure(EntityTypeBuilder<RoleModel> builder)
	{
		builder.ToHistoryTable("Role", SqlSchema.Identity, SqlSchema.History);

		builder.Property(p => p.Description)
			.HasMaxLength(500)
			.IsRequired(false);

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
		List<RoleType> roleTypes = RoleType.ADMINISTRATOR.GetValues().ToList();
		ICollection<RoleModel> listToReturn = [];

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
