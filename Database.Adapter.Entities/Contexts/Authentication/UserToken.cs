using Microsoft.AspNetCore.Identity;

namespace Database.Adapter.Entities.Contexts.Authentication;

/// <inheritdoc/>
public sealed class UserToken : IdentityUserToken<int>
{
}
