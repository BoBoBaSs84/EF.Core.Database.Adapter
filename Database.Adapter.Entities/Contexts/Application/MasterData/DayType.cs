using Database.Adapter.Entities.BaseTypes;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.Contexts.Application.MasterData;

/// <summary>
/// The day type enumerator entity type class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="EnumeratorModel"/> class.
/// </remarks>
[XmlRoot(ElementName = nameof(DayType))]
public partial class DayType : EnumeratorModel
{
}
