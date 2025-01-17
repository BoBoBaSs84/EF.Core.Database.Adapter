﻿using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserClaimEntity"/> entity.
/// </summary>
internal sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimEntity>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserClaimEntity> builder) =>
		builder.ToHistoryTable("UserClaim", SqlSchema.Identity, SqlSchema.History);
}
