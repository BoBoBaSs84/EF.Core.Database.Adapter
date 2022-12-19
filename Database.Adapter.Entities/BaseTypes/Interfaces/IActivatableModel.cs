namespace Database.Adapter.Entities.BaseTypes.Interfaces;

internal interface IActivatableModel
{
	/// <summary>The <see cref="IsActive"/> property.</summary>
	bool IsActive { get; }
	/// <summary>
	/// Should return false, if the <see cref="IsActive"/> property has the value <see langword="false"/>.
	/// </summary>
	/// <returns><see langword="true"/> or <see langword="false"/></returns>
	bool ShouldSerializeIsActive();
}
