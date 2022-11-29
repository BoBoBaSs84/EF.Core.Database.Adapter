using Database.Adapter.Entities.BaseTypes;

namespace Database.Adapter.Entities.MasterData;

/// <summary>
/// The calendar entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class.
/// </remarks>
public sealed class Calendar : IdentityModel
{
	/// <summary>The <see cref="Date"/> property.</summary>
	public DateTime Date { get; set; }
	/// <summary>The <see cref="Year"/> property.</summary>
	public int? Year { get; set; }
	/// <summary>The <see cref="Month"/> property.</summary>
	public int? Month { get; set; }
	/// <summary>The <see cref="Day"/> property.</summary>
	public int? Day { get; set; }
	/// <summary>The <see cref="Week"/> property.</summary>
	public int? Week { get; set; }
	/// <summary>The <see cref="IsoWeek"/> property.</summary>
	public int? IsoWeek { get; set; }
	/// <summary>The <see cref="DayOfYear"/> property.</summary>
	public int? DayOfYear { get; set; }
	/// <summary>The <see cref="WeekDay"/> property.</summary>
	public int? WeekDay { get; set; }
	/// <summary>The <see cref="EndOfMonth"/> property.</summary>
	public DateTime? EndOfMonth { get; set; }
	/// <summary>The <see cref="WeekDayName"/> property.</summary>
	public string? WeekDayName { get; set; }
	/// <summary>The <see cref="MonthName"/> property.</summary>
	public string? MonthName { get; set; }
}
