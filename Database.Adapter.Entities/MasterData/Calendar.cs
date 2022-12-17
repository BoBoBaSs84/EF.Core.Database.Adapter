using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.MasterData;

/// <summary>
/// The calendar entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class.
/// </remarks>
[Serializable]
[Index(nameof(Date), IsUnique = true)]
[XmlRoot(ElementName = nameof(Calendar), IsNullable = false)]
public sealed class Calendar : IdentityModel
{
	/// <summary>The <see cref="Date"/> property.</summary>
	[Column(TypeName = SqlDataType.DATE)]
	[JsonPropertyName(nameof(Date))]
	[XmlAttribute(AttributeName = nameof(Date))]
	public DateTime Date { get; set; } = default!;
	/// <summary>The <see cref="Year"/> property.</summary>
	[JsonPropertyName(nameof(Year))]
	[XmlAttribute(AttributeName = nameof(Year))]
	public int Year { get; set; } = default!;
	/// <summary>The <see cref="Month"/> property.</summary>
	[JsonPropertyName(nameof(Month))]
	[XmlAttribute(AttributeName = nameof(Month))]
	public int Month { get; set; } = default!;
	/// <summary>The <see cref="Day"/> property.</summary>
	[JsonPropertyName(nameof(Day))]
	[XmlAttribute(AttributeName = nameof(Day))]
	public int Day { get; set; } = default!;
	/// <summary>The <see cref="Week"/> property.</summary>
	[JsonPropertyName(nameof(Week))]
	[XmlAttribute(AttributeName = nameof(Week))]
	public int Week { get; set; } = default!;
	/// <summary>The <see cref="IsoWeek"/> property.</summary>
	[JsonPropertyName(nameof(IsoWeek))]
	[XmlAttribute(AttributeName = nameof(IsoWeek))]
	public int IsoWeek { get; set; } = default!;
	/// <summary>The <see cref="DayOfYear"/> property.</summary>
	[JsonPropertyName(nameof(DayOfYear))]
	[XmlAttribute(AttributeName = nameof(DayOfYear))]
	public int DayOfYear { get; set; } = default!;
	/// <summary>The <see cref="WeekDay"/> property.</summary>
	[JsonPropertyName(nameof(WeekDay))]
	[XmlAttribute(AttributeName = nameof(WeekDay))]
	public int WeekDay { get; set; } = default!;
	/// <summary>The <see cref="EndOfMonth"/> property.</summary>
	[JsonPropertyName(nameof(EndOfMonth))]
	[XmlAttribute(AttributeName = nameof(EndOfMonth))]
	public DateTime EndOfMonth { get; set; } = default!;
	/// <summary>The <see cref="WeekDayName"/> property.</summary>
	[JsonPropertyName(nameof(WeekDayName))]
	[XmlAttribute(AttributeName = nameof(WeekDayName))]
	public string WeekDayName { get; set; } = default!;
	/// <summary>The <see cref="MonthName"/> property.</summary>
	[JsonPropertyName(nameof(MonthName))]
	[XmlAttribute(AttributeName = nameof(MonthName))]
	public string MonthName { get; set; } = default!;
}
