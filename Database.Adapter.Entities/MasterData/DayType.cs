using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace Database.Adapter.Entities.MasterData;

/// <summary>
/// The day type enumerator entity type class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="EnumeratorModel"/> class.
/// </remarks>
[Serializable]
[Index(nameof(Name), IsUnique = true)]
[XmlRoot(ElementName = nameof(DayType), IsNullable = false)]
public sealed class DayType : EnumeratorModel
{
}
