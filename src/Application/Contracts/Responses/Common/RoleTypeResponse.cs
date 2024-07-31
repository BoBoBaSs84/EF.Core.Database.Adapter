using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The role type response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the role type response class.
/// </remarks>
/// <inheritdoc/>
public sealed class RoleTypeResponse(RoleType enumValue) : EnumeratorBaseResponse<RoleType>(enumValue)
{ }
