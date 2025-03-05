using BB84.Home.Application.Contracts.Responses.Common.Base;
using BB84.Home.Domain.Enumerators;

namespace BB84.Home.Application.Contracts.Responses.Common;

/// <summary>
/// The user role type response.
/// </summary>
/// <inheritdoc/>
public sealed class RoleTypeResponse(RoleType enumValue) : EnumeratorBaseResponse<RoleType>(enumValue)
{ }
