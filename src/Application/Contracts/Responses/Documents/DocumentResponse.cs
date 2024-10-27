using Application.Contracts.Responses.Base;

using Domain.Enumerators.Documents;

namespace Application.Contracts.Responses.Documents;

/// <summary>
/// The response for the document.
/// </summary>
public sealed class DocumentResponse : IdentityResponse
{
	/// <summary>
	/// The name of the document.
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	/// The directory of the document.
	/// </summary>
	public required string Directory { get; init; }

	/// <summary>
	/// The flags the of the document.
	/// </summary>
	public required DocumentTypes Flags { get; init; }

	/// <summary>
	/// The creation date of the document.
	/// </summary>
	public required DateTime CreationTime { get; init; }

	/// <summary>
	/// The last modification date of the document.
	/// </summary>
	public DateTime? LastWriteTime { get; init; }

	/// <summary>
	/// The last acces date of the document.
	/// </summary>
	public DateTime? LastAccessTime { get; init; }

	/// <summary>
	/// The <b>Message-Digest Algorithm 5</b> of the data.
	/// </summary>
	public required byte[] MD5Hash { get; set; }

	/// <summary>
	/// The length of the data content.
	/// </summary>
	public required long Length { get; set; }

	/// <summary>
	/// The actual content of the data.
	/// </summary>
	public required byte[] Content { get; set; }

	/// <summary>
	/// The name of the document extenion.
	/// </summary>
	public required string ExtenionName { get; set; }

	/// <summary>
	/// The mime type of the document extenion.
	/// </summary>
	public string? MimeType { get; set; }
}
