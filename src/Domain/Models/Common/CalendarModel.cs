using System.ComponentModel.DataAnnotations.Schema;

using Domain.Models.Base;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;

namespace Domain.Models.Common;

/// <summary>
/// The calendar model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityModel"/> class.
/// </remarks>
public partial class CalendarModel : IdentityModel
{
	/// <summary>
	/// The <see cref="Date"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.DATE)]
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
}
