using Database.Adapter.Entities.Contexts.Application.Timekeeping;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Database.Adapter.Entities.Contexts.Application.MasterData;

public partial class DayType
{
	[XmlElement(ElementName = nameof(CalendarDays), Form = XmlSchemaForm.Qualified)]
	private List<CalendarDay> calendarDays = default!;
	[XmlElement(ElementName = nameof(Attendances), Form = XmlSchemaForm.Qualified)]
	private List<Attendance> attendances = default!;

	/// <summary>The <see cref="CalendarDays"/> property.</summary>
	[XmlIgnore]
	public virtual ICollection<CalendarDay> CalendarDays
	{
		get => calendarDays;
		set => calendarDays = (List<CalendarDay>)value;
	}
	/// <summary>The <see cref="Attendances"/> property.</summary>
	[XmlIgnore]
	public virtual ICollection<Attendance> Attendances
	{
		get => attendances;
		set => attendances = (List<Attendance>)value;
	}
}
