using Database.Adapter.Entities.Contexts.Application.Authentication;
using Database.Adapter.Entities.Contexts.Application.MasterData;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.Contexts.Application.Timekeeping;

public partial class Attendance
{
	[XmlElement(ElementName = nameof(DayType), Form = XmlSchemaForm.Qualified)]
	private DayType dayType = default!;
	[XmlElement(ElementName = nameof(CalendarDay), Form = XmlSchemaForm.Qualified)]
	private CalendarDay calendarDay = default!;
	[XmlElement(ElementName = nameof(User), Form = XmlSchemaForm.Qualified)]
	private User user = default!;

	/// <summary>The <see cref="User"/> property.</summary>
	[XmlIgnore]
	public virtual User User { get => user; set => user = value; }
	/// <summary>The <see cref="CalendarDay"/> property.</summary>
	[XmlIgnore]
	public virtual CalendarDay CalendarDay { get => calendarDay; set => calendarDay = value; }
	/// <summary>The <see cref="DayType"/> property.</summary>
	[XmlIgnore]
	public virtual DayType DayType { get => dayType; set => dayType = value; }
}
