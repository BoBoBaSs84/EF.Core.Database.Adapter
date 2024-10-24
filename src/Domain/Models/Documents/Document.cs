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
	/// The data length of the document.
	/// </summary>
	public required long Length { get; set; }

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
	/// The Message-Digest Algorithm 5 of the document.
	/// </summary>
	public required byte[] MD5Hash { get; set; }

	/// <summary>
	/// The extension identifier of the document.
	/// </summary>
	public required Guid ExtensionId { get; set; }

	/// <summary>
	/// The extension of the document.
	/// </summary>
	public required DocumentExtension Extension { get; set; }
}
