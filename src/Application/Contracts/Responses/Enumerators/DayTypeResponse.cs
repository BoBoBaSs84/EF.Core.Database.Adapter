using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The day type response class.
/// </summary>
public sealed class DayTypeResponse : EnumeratorResponse<DayType>
{
	/// <summary>
	/// Initilizes an instance of the day type response class.
	/// </summary>
	/// <inheritdoc/>
	public DayTypeResponse(DayType enumValue) : base(enumValue)
	{ }
}
