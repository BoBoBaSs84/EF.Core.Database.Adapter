using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
[Serializable]
[Index(nameof(Date), IsUnique = true), Index(nameof(Year), IsUnique = false)]
[XmlRoot(ElementName = nameof(CalendarDay), IsNullable = false, Namespace = XmlNameSpaces.CALENDARDAY_NAMESPACE)]
public sealed class CalendarDay : IdentityModel
{
	/// <summary>The <see cref="Date"/> property.</summary>
	[Column(TypeName = SqlDataType.DATE)]
	[JsonPropertyName(nameof(Date))]
	[XmlElement(DataType = XmlDataType.DATE, ElementName = nameof(Date))]
	public DateTime Date { get; set; } = default!;
	/// <summary>The <see cref="Year"/> property.</summary>
	[JsonPropertyName(nameof(Year))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Year))]
	public int Year { get; set; } = default!;
	/// <summary>The <see cref="Month"/> property.</summary>
	[JsonPropertyName(nameof(Month))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Month))]
	public int Month { get; set; } = default!;
	/// <summary>The <see cref="Day"/> property.</summary>
	[JsonPropertyName(nameof(Day))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Day))]
	public int Day { get; set; } = default!;
	/// <summary>The <see cref="Week"/> property.</summary>
	[JsonPropertyName(nameof(Week))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(Week))]
	public int Week { get; set; } = default!;
	/// <summary>The <see cref="IsoWeek"/> property.</summary>
	[JsonPropertyName(nameof(IsoWeek))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(IsoWeek))]
	public int IsoWeek { get; set; } = default!;
	/// <summary>The <see cref="DayOfYear"/> property.</summary>
	[JsonPropertyName(nameof(DayOfYear))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(DayOfYear))]
	public int DayOfYear { get; set; } = default!;
	/// <summary>The <see cref="WeekDay"/> property.</summary>
	[JsonPropertyName(nameof(WeekDay))]
	[XmlElement(DataType = XmlDataType.INT, ElementName = nameof(WeekDay))]
	public int WeekDay { get; set; } = default!;
	/// <summary>The <see cref="EndOfMonth"/> property.</summary>
	[JsonPropertyName(nameof(EndOfMonth))]
	[XmlElement(DataType = XmlDataType.DATE, ElementName = nameof(EndOfMonth))]
	public DateTime EndOfMonth { get; set; } = default!;
	/// <summary>The <see cref="WeekDayName"/> property.</summary>
	[JsonPropertyName(nameof(WeekDayName))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(WeekDayName))]
	public string WeekDayName { get; set; } = default!;
	/// <summary>The <see cref="MonthName"/> property.</summary>
	[JsonPropertyName(nameof(MonthName))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(MonthName))]
	public string MonthName { get; set; } = default!;
}
