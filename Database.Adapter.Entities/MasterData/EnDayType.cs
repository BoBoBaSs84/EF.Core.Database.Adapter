using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.MasterData;

/// <summary>
/// The day type enumerator entity type class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="EnumeratorModel"/> class.
/// </remarks>
[Table(nameof(EnDayType), Schema = SqlSchema.PRIVATE)]
[Index(nameof(Name), IsUnique = true)]
[XmlRoot(ElementName = nameof(EnDayType), IsNullable = false)]
public sealed class EnDayType : EnumeratorModel
{
}
