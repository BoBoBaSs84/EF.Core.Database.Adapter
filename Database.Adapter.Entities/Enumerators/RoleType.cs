using Database.Adapter.Entities.Properties;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Properties.Resources;

namespace Database.Adapter.Entities.Enumerators;

/// <summary>
/// The role type enumerator.
/// </summary>
public enum RoleType
{
	/// <summary>The <see cref="ADMINISTRATOR"/> role type.</summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_Role_Administrator_Name),
		ShortName = nameof(Enumerator_Role_Administrator_ShortName),
		Description = nameof(Enumerator_Role_Administrator_Description))]
	ADMINISTRATOR = 1,
	/// <summary>The <see cref="USER"/> role type.</summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(Enumerator_Role_User_Name),
		ShortName = nameof(Enumerator_Role_User_ShortName),
		Description = nameof(Enumerator_Role_User_Description))]
	USER = 2,
	/// <summary>The <see cref="SUPERUSER"/> role type.</summary>
	[Display(ResourceType = typeof(Resources),
	Name = nameof(Enumerator_Role_SuperUser_Name),
	ShortName = nameof(Enumerator_Role_SuperUser_ShortName),
	Description = nameof(Enumerator_Role_SuperUser_Description))]
	SUPERUSER = 3
}
