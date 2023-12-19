using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The role type response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the role type response class.
/// </remarks>
/// <inheritdoc/>
public sealed class RoleTypeResponse(RoleType enumValue) : EnumeratorResponse<RoleType>(enumValue)
{ }
