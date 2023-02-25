using DA.Domain.Extensions;
using DA.Domain.Models.Identity;
using DA.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static DA.Domain.Constants.Sql.Schema;
using ERT = DA.Domain.Enumerators.ERT;

namespace DA.Infrastructure.Data.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class IdentityConfiguration
{
	/// <inheritdoc/>
	internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToSytemVersionedTable(nameof(User), IDENTITY);

			builder.HasMany(e => e.Claims)
				.WithOne(e => e.User)
				.HasForeignKey(ucl => ucl.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.Logins)
				.WithOne(e => e.User)
				.HasForeignKey(ulo => ulo.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.Tokens)
				.WithOne(e => e.User)
				.HasForeignKey(uto => uto.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.UserRoles)
				.WithOne(e => e.User)
				.HasForeignKey(uro => uro.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.Attendances)
				.WithOne(e => e.User)
				.HasForeignKey(uat => uat.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.User)
				.HasForeignKey(eca => eca.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);

			builder.HasMany(e => e.AccountUsers)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(true);
		}
	}

	/// <inheritdoc/>
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
			List<ERT> roleTypes = ERT.ADMINISTRATOR.GetListFromEnum();
			ICollection<Role> listToReturn = new List<Role>();

			foreach (ERT roleType in roleTypes)
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

	/// <inheritdoc/>
	internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
	{
		public void Configure(EntityTypeBuilder<RoleClaim> builder) =>
			builder.ToSytemVersionedTable(nameof(RoleClaim), IDENTITY);
	}

	/// <inheritdoc/>
	internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
	{
		public void Configure(EntityTypeBuilder<UserClaim> builder) =>
			builder.ToSytemVersionedTable(nameof(UserClaim), IDENTITY);
	}

	/// <inheritdoc/>
	internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
	{
		public void Configure(EntityTypeBuilder<UserLogin> builder) =>
			builder.ToSytemVersionedTable(nameof(UserLogin), IDENTITY);
	}

	/// <inheritdoc/>
	internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		public void Configure(EntityTypeBuilder<UserRole> builder) =>
			builder.ToSytemVersionedTable(nameof(UserRole), IDENTITY);
	}

	/// <inheritdoc/>
	internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder) =>
			builder.ToSytemVersionedTable(nameof(UserToken), IDENTITY);
	}
}
