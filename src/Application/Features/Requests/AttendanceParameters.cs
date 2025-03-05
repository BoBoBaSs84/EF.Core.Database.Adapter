using Application.Features.Requests.Base;

using BB84.Home.Domain.Enumerators.Attendance;

namespace Application.Features.Requests;

/// <summary>
/// The parameters for the attendance request.
/// </summary>
public sealed class AttendanceParameters : RequestParameters
{
	/// <summary>
	/// Filter option by the year.
	/// </summary>
	public int? Year { get; init; }

	/// <summary>
	/// Filter option by the month.
	/// </summary>
	public int? Month { get; init; }

	/// <summary>
	/// Filter option by the minimum date.
	/// </summary>	
	public DateTime? MinDate { get; init; }

	/// <summary>
	/// Filter option by the maximum date.
	/// </summary>
	public DateTime? MaxDate { get; init; }

	/// <summary>
	/// Filter option by the attendance type.
	/// </summary>
	public AttendanceType? Type { get; init; }
}
