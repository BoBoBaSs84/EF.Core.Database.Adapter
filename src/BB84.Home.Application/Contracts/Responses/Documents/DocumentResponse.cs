using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using BB84.Home.Application.Contracts.Responses.Base;
using BB84.Home.Application.Converters;
using BB84.Home.Domain.Enumerators.Documents;

namespace BB84.Home.Application.Contracts.Responses.Documents;

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
	[JsonConverter(typeof(FlagsJsonConverterFactory))]
	public required DocumentTypes Flags { get; init; }

	/// <summary>
	/// The creation date of the document.
	/// </summary>
	[DataType(DataType.DateTime)]
	public required DateTime CreationTime { get; init; }

	/// <summary>
	/// The last modification date of the document.
	/// </summary>
	[DataType(DataType.DateTime)]
	public DateTime? LastWriteTime { get; init; }

	/// <summary>
	/// The last acces date of the document.
	/// </summary>
	[DataType(DataType.DateTime)]
	public DateTime? LastAccessTime { get; init; }

	/// <summary>
	/// The <b>Message-Digest Algorithm 5</b> of the data.
	/// </summary>
	public byte[]? MD5Hash { get; init; }

	/// <summary>
	/// The length of the data content.
	/// </summary>
	public long? Length { get; init; }

	/// <summary>
	/// The actual content of the data.
	/// </summary>
	public byte[]? Content { get; init; }

	/// <summary>
	/// The name of the document extension.
	/// </summary>
	public string? ExtensionName { get; init; }

	/// <summary>
	/// The mime type of the document extenion.
	/// </summary>
	public string? MimeType { get; init; }
}
