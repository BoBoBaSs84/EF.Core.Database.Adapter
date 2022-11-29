namespace Database.Adapter.Entities.BaseTypes.Interfaces;

internal interface IAuditedModel
{
	/// <summary>The <see cref="CreatedBy"/> property.</summary>
	Guid CreatedBy { get; }
	/// <summary>The <see cref="ModifiedBy"/> property.</summary>
	Guid? ModifiedBy { get; }
}
