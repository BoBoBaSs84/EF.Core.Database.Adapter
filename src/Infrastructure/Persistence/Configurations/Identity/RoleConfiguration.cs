using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Extensions;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="RoleEntity"/> entity.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
	public void Configure(EntityTypeBuilder<RoleEntity> builder)
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

	private static ICollection<RoleEntity> GetRoleTypes()
	{
		List<RoleType> roleTypes = RoleType.ADMINISTRATOR.GetValues().ToList();
		ICollection<RoleEntity> listToReturn = [];

		foreach (RoleType roleType in roleTypes)
			listToReturn.Add(new RoleEntity()
			{
				Id = Guid.NewGuid(),
				Name = roleType.GetName(),
				NormalizedName = roleType.ToString(),
				Description = roleType.GetDescription()
			});
		return listToReturn;
	}
}
