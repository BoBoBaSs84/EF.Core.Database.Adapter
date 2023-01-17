namespace Database.Adapter.Entities.BaseTypes.Interfaces;

internal interface IAuditedModel
{
	/// <summary>The <see cref="CreatedBy"/> property.</summary>
	int CreatedBy { get; }
	/// <summary>The <see cref="ModifiedBy"/> property.</summary>
	int? ModifiedBy { get; }
}
