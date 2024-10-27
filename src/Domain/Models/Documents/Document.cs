using BB84.EntityFrameworkCore.Models;

using Domain.Enumerators.Documents;

namespace Domain.Models.Documents;

/// <summary>
/// The document entity class.
/// </summary>
public sealed class Document : AuditedModel
{
	/// <summary>
	/// The name of the document.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// The directory of the document.
	/// </summary>
	public required string Directory { get; set; }

	/// <summary>
	/// The flags the of the document.
	/// </summary>
	public required DocumentTypes Flags { get; set; }

	/// <summary>
	/// The creation date of the document.
	/// </summary>
	public required DateTime CreationTime { get; set; }

	/// <summary>
	/// The last modification date of the document.
	/// </summary>
	public DateTime? LastWriteTime { get; set; }

	/// <summary>
	/// The last acces date of the document.
	/// </summary>
	public DateTime? LastAccessTime { get; set; }

	/// <summary>
	/// The extension identifier of the document.
	/// </summary>
	public Guid ExtensionId { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="Extension"/> property.
	/// </summary>
	public Extension Extension { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="DocumentDatas"/> property.
	/// </summary>
	public ICollection<DocumentData> DocumentDatas { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="DocumentUsers"/> property.
	/// </summary>
	public ICollection<DocumentUser> DocumentUsers { get; set; } = default!;
}
