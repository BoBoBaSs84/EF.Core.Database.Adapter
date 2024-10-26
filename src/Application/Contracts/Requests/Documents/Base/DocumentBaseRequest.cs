using Domain.Enumerators.Documents;

namespace Application.Contracts.Requests.Documents.Base;

/// <summary>
/// The base request for creating or updating a document.
/// </summary>
public abstract class DocumentBaseRequest
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
	/// The data length of the document.
	/// </summary>
	public required long Length { get; init; }

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
	/// The <b>Message-Digest Algorithm 5</b> of the document.
	/// </summary>
	public required byte[] MD5Hash { get; init; }

	/// <summary>
	/// The name of the document extenion.
	/// </summary>
	public required string ExtensionName { get; init; }

	/// <summary>
	/// The actual data of the document.
	/// </summary>
	public required byte[] Data { get; init; }
}
