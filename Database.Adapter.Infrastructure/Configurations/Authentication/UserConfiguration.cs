using Database.Adapter.Entities.Contexts.Application.Authentication;
using Database.Adapter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using static Database.Adapter.Entities.Constants.SqlConstants.SqlSchema;

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
			.HasForeignKey(uc => uc.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasMany(e => e.Logins)
			.WithOne(e => e.User)
			.HasForeignKey(ul => ul.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasMany(e => e.Tokens)
			.WithOne(e => e.User)
			.HasForeignKey(ut => ut.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasMany(e => e.UserRoles)
			.WithOne(e => e.User)
			.HasForeignKey(ur => ur.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);

		builder.HasMany(e => e.Attendances)
			.WithOne(e => e.User)
			.HasForeignKey(ua => ua.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(true);
	}
}
