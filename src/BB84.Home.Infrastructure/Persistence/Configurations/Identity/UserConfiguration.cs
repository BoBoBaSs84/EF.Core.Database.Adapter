﻿using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Infrastructure.Persistence.Converters;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static BB84.Home.Infrastructure.Common.InfrastructureConstants;

namespace BB84.Home.Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserEntity"/> entity.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserEntity> builder)
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
			.IsDateColumn();

		builder.Property(e => e.Preferences)
			.HasConversion<PreferencesConverter>()
			.IsXmlColumn();

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
