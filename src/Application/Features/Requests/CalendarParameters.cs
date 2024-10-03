using System.ComponentModel.DataAnnotations;

using Application.Features.Requests.Base;

using CDR = Application.Common.Constants.DateRanges;

namespace Application.Features.Requests;

/// <summary>
/// The calendar request parameter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="RequestParameters"/> class.
/// </remarks>
public sealed class CalendarParameters : RequestParameters
{
	/// <summary>
	/// Filter option by the year.
	/// </summary>
	[Range(CDR.MinYear, CDR.MaxYear)]
	public int? Year { get; set; }

	/// <summary>
	/// Filter option by the month.
	/// </summary>
	[Range(CDR.MinMonth, CDR.MaxMonth)]
	public int? Month { get; set; }

	/// <summary>
	/// Filter option by the minimum date.
	/// </summary>
	public DateTime? MinDate { get; set; }

	/// <summary>
	/// Filter option by the maximum date.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? MaxDate { get; set; }
}
