using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The work day type response class.
/// </summary>
public sealed class WorkDayTypeResponse : EnumeratorResponse<WorkDayTypes>
{
	/// <summary>
	/// Initilizes an instance of the work day type response class.
	/// </summary>
	/// <inheritdoc/>
	public WorkDayTypeResponse(WorkDayTypes enumValue) : base(enumValue)
	{ }
}
