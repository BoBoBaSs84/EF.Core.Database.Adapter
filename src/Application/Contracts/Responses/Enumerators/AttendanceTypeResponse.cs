using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The attendance type response class.
/// </summary>
public sealed class AttendanceTypeResponse : EnumeratorResponse<AttendanceType>
{
	/// <summary>
	/// Initilizes an instance of the attendance type response class.
	/// </summary>
	/// <inheritdoc/>
	public AttendanceTypeResponse(AttendanceType enumValue) : base(enumValue)
	{ }
}
