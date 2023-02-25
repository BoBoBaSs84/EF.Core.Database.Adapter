using DA.Domain.Models.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static DA.Domain.Constants.Sql;

namespace DA.Domain.Models.MasterData;

/// <summary>
/// The calendar entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class.
/// </remarks>
[Index(nameof(Date), IsUnique = true), Index(nameof(Year), IsUnique = false)]
public partial class CalendarDay : IdentityModel
{
	/// <summary>
	/// The <see cref="Date"/> property.
	/// </summary>
	[Column(TypeName = DataType.DATE)]
	public DateTime Date { get; set; } = default!;
	/// <summary>
	/// The <see cref="Year"/> property.
	/// </summary>
	public int Year { get; private set; } = default!;
	/// <summary>
	/// The <see cref="Month"/> property.
	/// </summary>
	public int Month { get; private set; } = default!;
	/// <summary>
	/// The <see cref="Day"/> property.
	/// </summary>
	public int Day { get; private set; } = default!;
	/// <summary>
	/// The <see cref="Week"/> property.
	/// </summary>
	public int Week { get; private set; } = default!;
	/// <summary>
	/// The <see cref="IsoWeek"/> property.
	/// </summary>
	public int IsoWeek { get; private set; } = default!;
	/// <summary>
	/// The <see cref="DayOfYear"/> property.
	/// </summary>
	public int DayOfYear { get; private set; } = default!;
	/// <summary>
	/// The <see cref="WeekDay"/> property.
	/// </summary>
	public int WeekDay { get; private set; } = default!;
	/// <summary>
	/// The <see cref="EndOfMonth"/> property.
	/// </summary>
	public DateTime EndOfMonth { get; private set; } = default!;
	/// <summary>
	/// The <see cref="WeekDayName"/> property.
	/// </summary>
	public string WeekDayName { get; private set; } = default!;
	/// <summary>
	/// The <see cref="MonthName"/> property.
	/// </summary>
	public string MonthName { get; private set; } = default!;
	/// <summary>
	/// The <see cref="DayTypeId"/> property.
	/// </summary>
	public int DayTypeId { get; set; } = default!;
}
