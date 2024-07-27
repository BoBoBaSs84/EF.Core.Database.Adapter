using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Models.Identity;

using Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

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

			builder.Property(e => e.Preferences)
				.HasConversion<PreferencesConverter>()
				.HasColumnType(SqlDataType.XML);

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
