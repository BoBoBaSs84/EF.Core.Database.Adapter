﻿using System.ComponentModel.DataAnnotations;

using RESX = BB84.Home.Domain.Properties.EnumeratorResources;

namespace BB84.Home.Domain.Enumerators;

/// <summary>
/// The role type enumerator.
/// </summary>
public enum RoleType : byte
{
	/// <summary>
	/// The <see cref="ADMINISTRATOR"/> role type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.RoleType_Administrator_Name),
		ShortName = nameof(RESX.RoleType_Administrator_ShortName),
		Description = nameof(RESX.RoleType_Administrator_Description))]
	ADMINISTRATOR = 1,
	/// <summary>
	/// The <see cref="USER"/> role type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.RoleType_User_Name),
		ShortName = nameof(RESX.RoleType_User_ShortName),
		Description = nameof(RESX.RoleType_User_Description))]
	USER,
	/// <summary>
	/// The <see cref="SUPERUSER"/> role type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
	Name = nameof(RESX.RoleType_SuperUser_Name),
	ShortName = nameof(RESX.RoleType_SuperUser_ShortName),
	Description = nameof(RESX.RoleType_SuperUser_Description))]
	SUPERUSER
}
