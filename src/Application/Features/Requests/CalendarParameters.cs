using System.ComponentModel.DataAnnotations;

using Application.Features.Requests.Base;

using DR = Application.Common.Statics.DateRanges;

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
	[Range(DR.MinYear, DR.MaxYear)]
	public int? Year { get; set; }

	/// <summary>
	/// Filter option by the month.
	/// </summary>
	[Range(DR.MinMonth, DR.MaxMonth)]
	public int? Month { get; set; }

	/// <summary>
	/// Filter option by the minimum date.
	/// </summary>	
	[DataType(DataType.Date)]
	public DateTime? MinDate { get; set; }

	/// <summary>
	/// Filter option by the maximum date.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? MaxDate { get; set; }
}
