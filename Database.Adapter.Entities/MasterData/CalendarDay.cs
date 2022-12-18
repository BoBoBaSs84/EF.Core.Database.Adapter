using System.Xml.Schema;
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
	[XmlAttribute(AttributeName = nameof(Date), DataType = XmlDataType.DATE)]
	public DateTime Date { get; set; } = default!;
	/// <summary>The <see cref="Year"/> property.</summary>
	[JsonPropertyName(nameof(Year))]
	[XmlAttribute(AttributeName = nameof(Year), DataType = XmlDataType.INT)]
	public int Year { get; set; } = default!;
	/// <summary>The <see cref="Month"/> property.</summary>
	[JsonPropertyName(nameof(Month))]
	[XmlAttribute(AttributeName = nameof(Month), DataType = XmlDataType.INT)]
	public int Month { get; set; } = default!;
	/// <summary>The <see cref="Day"/> property.</summary>
	[JsonPropertyName(nameof(Day))]
	[XmlAttribute(AttributeName = nameof(Day), DataType = XmlDataType.INT)]
	public int Day { get; set; } = default!;
	/// <summary>The <see cref="Week"/> property.</summary>
	[JsonPropertyName(nameof(Week))]
	[XmlAttribute(AttributeName = nameof(Week), DataType = XmlDataType.INT)]
	public int Week { get; set; } = default!;
	/// <summary>The <see cref="IsoWeek"/> property.</summary>
	[JsonPropertyName(nameof(IsoWeek))]
	[XmlAttribute(AttributeName = nameof(IsoWeek), DataType = XmlDataType.INT)]
	public int IsoWeek { get; set; } = default!;
	/// <summary>The <see cref="DayOfYear"/> property.</summary>
	[JsonPropertyName(nameof(DayOfYear))]
	[XmlAttribute(AttributeName = nameof(DayOfYear), DataType = XmlDataType.INT)]
	public int DayOfYear { get; set; } = default!;
	/// <summary>The <see cref="WeekDay"/> property.</summary>
	[JsonPropertyName(nameof(WeekDay))]
	[XmlAttribute(AttributeName = nameof(WeekDay), DataType = XmlDataType.INT)]
	public int WeekDay { get; set; } = default!;
	/// <summary>The <see cref="EndOfMonth"/> property.</summary>
	[JsonPropertyName(nameof(EndOfMonth))]
	[XmlAttribute(AttributeName = nameof(EndOfMonth), DataType = XmlDataType.DATE)]
	public DateTime EndOfMonth { get; set; } = default!;
	/// <summary>The <see cref="WeekDayName"/> property.</summary>
	[JsonPropertyName(nameof(WeekDayName))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(WeekDayName), Form = XmlSchemaForm.Qualified)]
	public string WeekDayName { get; set; } = default!;
	/// <summary>The <see cref="MonthName"/> property.</summary>
	[JsonPropertyName(nameof(MonthName))]
	[XmlElement(DataType = XmlDataType.STRING, ElementName = nameof(MonthName), Form = XmlSchemaForm.Qualified)]
	public string MonthName { get; set; } = default!;
}
