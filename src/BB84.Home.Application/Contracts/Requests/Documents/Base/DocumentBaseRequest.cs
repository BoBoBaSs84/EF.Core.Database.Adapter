﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Enumerators.Documents;

namespace BB84.Home.Application.Contracts.Requests.Documents.Base;

/// <summary>
/// The base request for creating or updating a document.
/// </summary>
public abstract class DocumentBaseRequest
{
	/// <summary>
	/// The name of the document without extension.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string Name { get; init; }

	/// <summary>
	/// The extension name of the document without the <b>dot</b>.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string ExtensionName { get; init; }

	/// <summary>
	/// The directory of the document.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string Directory { get; init; }

	/// <summary>
	/// The flags the of the document.
	/// </summary>
	[Required]
	public required DocumentTypes Flags { get; init; }

	/// <summary>
	/// The creation date of the document.
	/// </summary>
	[Required, DataType(DataType.DateTime)]
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
	/// The actual data of the document.
	/// </summary>
	[Required]
	public required byte[] Content { get; init; }
}
