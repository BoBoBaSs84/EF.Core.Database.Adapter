using Application.Features.Requests.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Requests;

/// <summary>
/// The attendance request parameter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="RequestParameters"/> class.
/// </remarks>
public sealed class AttendanceParameters : RequestParameters
{
	/// <summary>
	/// The year to be filtered.
	/// </summary>
	[Range(1900, 2100)]
	public int? Year { get; set; }

	/// <summary>
	/// The month to be filtered.
	/// </summary>
	[Range(1, 12)]
	public int? Month { get; set; }

	/// <summary>
	/// The minimum date to be filtered.
	/// </summary>	
	[DataType(DataType.Date)]
	public DateTime? MinDate { get; set; }

	/// <summary>
	/// The maximum date to be filtered.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? MaxDate { get; set; }
}
