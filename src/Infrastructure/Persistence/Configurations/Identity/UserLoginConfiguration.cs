using BB84.EntityFrameworkCore.Repositories.SqlServer.Extensions;

using Domain.Entities.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Infrastructure.Common.InfrastructureConstants;

namespace Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// The configuration for the <see cref="UserLoginModel"/> entity.
/// </summary>
internal sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLoginModel>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<UserLoginModel> builder) =>
		builder.ToHistoryTable("UserLogin", SqlSchema.Identity, SqlSchema.History);
}