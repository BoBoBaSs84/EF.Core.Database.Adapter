using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.MasterData;

/// <summary>
/// The calendar entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class.
/// </remarks>
[Index(nameof(Date), IsUnique = true), Index(nameof(Year), IsUnique = false)]
[XmlRoot(ElementName = nameof(CalendarDay), IsNullable = false, Namespace = XmlNameSpaces.CALENDARDAY_NAMESPACE)]
public sealed class CalendarDay : IdentityModel
{
	/// <summary>The <see cref="Date"/> property.</summary>
	[Column(TypeName = SqlDataType.DATE)]
	[XmlElement(DataType = XmlDataType.DATE, ElementName = nameof(Date))]
	public DateTime Date { get; set; } = default!;
	/// <summary>The <see cref="Year"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Year))]
	public int Year { get; set; } = default!;
	/// <summary>The <see cref="Month"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Month))]
	public int Month { get; set; } = default!;
	/// <summary>The <see cref="Day"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Day))]
	public int Day { get; set; } = default!;
	/// <summary>The <see cref="Week"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Week))]
	public int Week { get; set; } = default!;
	/// <summary>The <see cref="IsoWeek"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(IsoWeek))]
	public int IsoWeek { get; set; } = default!;
	/// <summary>The <see cref="DayOfYear"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(DayOfYear))]
	public int DayOfYear { get; set; } = default!;
	/// <summary>The <see cref="WeekDay"/> property.</summary>
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(WeekDay))]
	public int WeekDay { get; set; } = default!;
	/// <summary>The <see cref="EndOfMonth"/> property.</summary>
	[XmlElement(DataType = XmlDataType.DATE, ElementName = nameof(EndOfMonth))]
	public DateTime EndOfMonth { get; set; } = default!;
	/// <summary>The <see cref="WeekDayName"/> property.</summary>
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(WeekDayName))]
	public string WeekDayName { get; set; } = default!;
	/// <summary>The <see cref="MonthName"/> property.</summary>
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(MonthName))]
	public string MonthName { get; set; } = default!;

	/// <summary>The <see cref="DayTypeId"/> property.</summary>
	[XmlIgnore]
	[ForeignKey(nameof(DayType))]
	public int DayTypeId { get; set; } = default!;
	/// <summary>The <see cref="DayType"/> property.</summary>
	[XmlElement(ElementName = nameof(DayType), IsNullable = false, Namespace = XmlNameSpaces.ENUMERATOR_NAMSPACE)]
	public DayType DayType { get; set; } = default!;
}
