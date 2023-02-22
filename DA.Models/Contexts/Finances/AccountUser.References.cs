﻿using DA.Models.Contexts.Authentication;

namespace DA.Models.Contexts.Finances;

public partial class AccountUser
{
	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public virtual Account Account { get; set; } = default!;
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual User User { get; set; } = default!;
}