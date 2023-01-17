using Database.Adapter.Entities.Contexts.Application.Timekeeping;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.Contexts.Application.MasterData;

public partial class CalendarDay
{
	[XmlElement(ElementName = nameof(Attendances), Form = XmlSchemaForm.Qualified)]
	private List<Attendance> attendances = default!;
	[XmlElement(ElementName = nameof(DayType), Form = XmlSchemaForm.Qualified)]
	private DayType dayType = default!;

	/// <summary>The <see cref="DayType"/> property.</summary>
	[XmlIgnore]
	public virtual DayType DayType
	{
		get => dayType;
		set => dayType = value;
	}
	/// <summary>The <see cref="Attendances"/> property.</summary>
	[XmlIgnore]
	public virtual ICollection<Attendance> Attendances
	{
		get => attendances;
		set => attendances = (List<Attendance>)value;
	}
}
