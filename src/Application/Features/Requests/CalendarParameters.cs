﻿using System.ComponentModel.DataAnnotations;

using Application.Features.Requests.Base;

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
	[Range(1900, 9999)]
	public int? Year { get; set; }

	/// <summary>
	/// Filter option by the month.
	/// </summary>
	[Range(1, 12)]
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

	/// <summary>
	/// Filter option by the end of month.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? EndOfMonth { get; set; }
}
