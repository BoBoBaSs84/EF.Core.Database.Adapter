﻿namespace DA.Models.BaseTypes.Interfaces;

internal interface IActivatableModel
{
	/// <summary>The <see cref="IsActive"/> property.</summary>
	bool IsActive { get; }
}