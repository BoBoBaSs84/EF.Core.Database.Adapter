using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;
using static Database.Adapter.Entities.Constants.XmlConstants;

namespace Database.Adapter.Entities.Contexts.MasterData;

/// <summary>
/// The calendar entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="IdentityModel"/> class.
/// </remarks>
[Index(nameof(Date), IsUnique = true), Index(nameof(Year), IsUnique = false)]
[XmlRoot(ElementName = nameof(CalendarDay), Namespace = XmlNameSpaces.CALENDARDAY_NAMESPACE)]
public sealed class CalendarDay : IdentityModel
{
	/// <summary>The <see cref="Date"/> property.</summary>
	[Column(TypeName = SqlDataType.DATE)]
	[XmlAttribute(AttributeName = nameof(Date), DataType = XmlDataType.DATE, Form = XmlSchemaForm.Qualified)]
	public DateTime Date { get; set; } = default!;
	/// <summary>The <see cref="Year"/> property.</summary>
	[XmlIgnore]
	public int Year { get; private set; } = default!;
	/// <summary>The <see cref="Month"/> property.</summary>
	[XmlIgnore]
	public int Month { get; private set; } = default!;
	/// <summary>The <see cref="Day"/> property.</summary>
	[XmlIgnore]
	public int Day { get; private set; } = default!;
	/// <summary>The <see cref="Week"/> property.</summary>
	[XmlIgnore]
	public int Week { get; private set; } = default!;
	/// <summary>The <see cref="IsoWeek"/> property.</summary>
	[XmlIgnore]
	public int IsoWeek { get; private set; } = default!;
	/// <summary>The <see cref="DayOfYear"/> property.</summary>
	[XmlIgnore]
	public int DayOfYear { get; private set; } = default!;
	/// <summary>The <see cref="WeekDay"/> property.</summary>
	[XmlIgnore]
	public int WeekDay { get; private set; } = default!;
	/// <summary>The <see cref="EndOfMonth"/> property.</summary>
	[XmlIgnore]
	public DateTime EndOfMonth { get; private set; } = default!;
	/// <summary>The <see cref="WeekDayName"/> property.</summary>
	[XmlIgnore]
	public string WeekDayName { get; private set; } = default!;
	/// <summary>The <see cref="MonthName"/> property.</summary>
	[XmlIgnore]
	public string MonthName { get; private set; } = default!;

	/// <summary>The <see cref="DayTypeId"/> property.</summary>
	[XmlIgnore]
	[ForeignKey(nameof(DayType))]
	public int DayTypeId { get; set; } = default!;
	/// <summary>The <see cref="DayType"/> property.</summary>
	[XmlElement(ElementName = nameof(DayType))]
	public DayType DayType { get; set; } = default!;
}
