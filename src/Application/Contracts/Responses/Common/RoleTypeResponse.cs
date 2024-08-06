using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The user role type response.
/// </summary>
/// <inheritdoc/>
public sealed class RoleTypeResponse(RoleType enumValue) : EnumeratorBaseResponse<RoleType>(enumValue)
{ }
