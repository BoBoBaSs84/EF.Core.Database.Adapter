using Database.Adapter.Entities.Properties;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Properties.EnumeratorResources;

namespace Database.Adapter.Entities.Enumerators;

/// <summary>
/// The role type enumerator.
/// </summary>
public enum RoleType
{
	/// <summary>
	/// The <see cref="ADMINISTRATOR"/> role type.
	/// </summary>
	[Display(ResourceType = typeof(EnumeratorResources),
		Name = nameof(RoleType_Administrator_Name),
		ShortName = nameof(RoleType_Administrator_ShortName),
		Description = nameof(RoleType_Administrator_Description))]
	ADMINISTRATOR = 1,
	/// <summary>
	/// The <see cref="USER"/> role type.
	/// </summary>
	[Display(ResourceType = typeof(EnumeratorResources),
		Name = nameof(RoleType_User_Name),
		ShortName = nameof(RoleType_User_ShortName),
		Description = nameof(RoleType_User_Description))]
	USER,
	/// <summary>
	/// The <see cref="SUPERUSER"/> role type.
	/// </summary>
	[Display(ResourceType = typeof(EnumeratorResources),
	Name = nameof(RoleType_SuperUser_Name),
	ShortName = nameof(RoleType_SuperUser_ShortName),
	Description = nameof(RoleType_SuperUser_Description))]
	SUPERUSER
}
