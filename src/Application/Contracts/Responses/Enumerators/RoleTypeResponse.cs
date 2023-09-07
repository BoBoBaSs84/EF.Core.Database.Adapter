using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The role type response class.
/// </summary>
public sealed class RoleTypeResponse : EnumeratorResponse<RoleType>
{
	/// <summary>
	/// Initilizes an instance of the role type response class.
	/// </summary>
	/// <inheritdoc/>
	public RoleTypeResponse(RoleType enumValue) : base(enumValue)
	{ }
}
