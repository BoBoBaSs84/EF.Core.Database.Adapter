using Database.Adapter.Entities.BaseTypes;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.MasterData;

/// <summary>
/// The day type enumerator entity type class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="EnumeratorModel"/> class.
/// </remarks>
[XmlRoot(ElementName = nameof(DayType), IsNullable = false)]
public sealed class DayType : EnumeratorModel
{
	/// <summary>The <see cref="CalendarDays"/> property.</summary>
	[XmlIgnore]
	public List<CalendarDay> CalendarDays { get; set; } = default!;
}
