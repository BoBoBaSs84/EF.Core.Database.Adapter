using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Identity;

using Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static partial class IdentityConfiguration
{
	/// <inheritdoc/>
	internal sealed class UserConfiguration : IEntityTypeConfiguration<UserModel>
	{
		public void Configure(EntityTypeBuilder<UserModel> builder)
		{
			builder.ToHistoryTable("User", SqlSchema.Identity, SqlSchema.History);

			builder.Property(p => p.FirstName)
				.HasMaxLength(100);

			builder.Property(p => p.MiddleName)
				.HasMaxLength(100)
				.IsRequired(false);

			builder.Property(p => p.LastName)
				.HasMaxLength(100);

			builder.Property(p => p.DateOfBirth)
				.HasColumnType("date");

			builder.Property(e => e.Preferences)
				.HasConversion<PreferencesConverter>()
				.HasColumnType("xml");

			builder.HasMany(e => e.Claims)
				.WithOne(e => e.User)
				.HasForeignKey(ucl => ucl.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Logins)
				.WithOne(e => e.User)
				.HasForeignKey(ulo => ulo.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Tokens)
				.WithOne(e => e.User)
				.HasForeignKey(uto => uto.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.UserRoles)
				.WithOne(e => e.User)
				.HasForeignKey(uro => uro.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Attendances)
				.WithOne(e => e.User)
				.HasForeignKey(uat => uat.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.Cards)
				.WithOne(e => e.User)
				.HasForeignKey(eca => eca.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(e => e.AccountUsers)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
