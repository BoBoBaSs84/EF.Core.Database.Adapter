﻿using BB84.EntityFrameworkCore.Entities;

using Domain.Enumerators.Documents;
using Domain.Models.Identity;

namespace Domain.Entities.Documents;

/// <summary>
/// The document entity class.
/// </summary>
public sealed class DocumentEntity : AuditedEntity
{
	/// <summary>
	/// The name of the document.
	/// </summary>
	public string Name { get; set; } = default!;

	/// <summary>
	/// The directory of the document.
	/// </summary>
	public string Directory { get; set; } = default!;

	/// <summary>
	/// The flags the of the document.
	/// </summary>
	public DocumentTypes Flags { get; set; }

	/// <summary>
	/// The creation date of the document.
	/// </summary>
	public DateTime CreationTime { get; set; }

	/// <summary>
	/// The last modification date of the document.
	/// </summary>
	public DateTime? LastWriteTime { get; set; }

	/// <summary>
	/// The last acces date of the document.
	/// </summary>
	public DateTime? LastAccessTime { get; set; }

	/// <summary>
	/// The data identifier of the document.
	/// </summary>
	public Guid DataId { get; set; }

	/// <summary>
	/// The extension identifier of the document.
	/// </summary>
	public Guid ExtensionId { get; set; }

	/// <summary>
	/// The user identifier of the document.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The navigational <see cref="Extension"/> property.
	/// </summary>
	public ExtensionEntity Extension { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="Data"/> property.
	/// </summary>
	public DataEntity Data { get; set; } = default!;

	/// <summary>
	/// The navigational <see cref="User"/> property.
	/// </summary>
	public UserModel User { get; set; } = default!;
}