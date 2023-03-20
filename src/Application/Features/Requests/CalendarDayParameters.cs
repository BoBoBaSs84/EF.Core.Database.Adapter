using Application.Features.Requests.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Requests;

/// <summary>
/// The calendar request parameter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="RequestParameters"/> class.
/// </remarks>
public sealed class CalendarDayParameters : RequestParameters
{
	/// <summary>
	/// The year to be filtered.
	/// </summary>
	public int? Year { get; set; }

	/// <summary>
	/// The month to be filtered.
	/// </summary>
	[Range(1, 12)]
	public int? Month { get; set; }
}
