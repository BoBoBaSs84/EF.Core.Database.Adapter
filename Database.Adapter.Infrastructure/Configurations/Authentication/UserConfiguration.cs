using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.Sql.Schema;

namespace Database.Adapter.Infrastructure.Configurations.Authentication;

/// <inheritdoc/>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
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
