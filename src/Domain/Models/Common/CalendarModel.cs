using System.ComponentModel.DataAnnotations.Schema;

using Domain.Models.Base;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;

namespace Domain.Models.Common;

/// <summary>
/// The calendar model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityModel"/> class.
/// </remarks>
public partial class CalendarModel : IdentityModel
{
	/// <summary>
	/// The date property.
	/// </summary>
	[Column(TypeName = SqlDataType.DATE)]
	public DateTime Date { get; set; }
}
