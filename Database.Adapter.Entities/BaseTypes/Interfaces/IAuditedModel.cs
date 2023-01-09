namespace Database.Adapter.Entities.BaseTypes.Interfaces;

internal interface IAuditedModel
{
	/// <summary>The <see cref="CreatedBy"/> property.</summary>
	int CreatedBy { get; }
	/// <summary>The <see cref="ModifiedBy"/> property.</summary>
	int? ModifiedBy { get; }
	/// <summary>
	/// Should return false, if the <see cref="ModifiedBy"/> property is <see langword="null"/>.
	/// </summary>
	/// <returns><see langword="true"/> or <see langword="false"/></returns>
	bool ShouldSerializeModifiedBy();
}
