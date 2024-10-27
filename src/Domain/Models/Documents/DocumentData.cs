using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Documents;

/// <summary>
/// The document to data entity.
/// </summary>
public sealed class DocumentData : AuditedModel
{
	/// <summary>
	/// The <see cref="DocumentId"/> property.
	/// </summary>
	public Guid DocumentId { get; set; } = default!;

	/// <summary>
	/// The <see cref="DataId"/> property.
	/// </summary>
	public Guid DataId { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="Document"/> property.
	/// </summary>
	public Document Document { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="Data"/> property.
	/// </summary>
	public Data Data { get; set; } = default!;
}
