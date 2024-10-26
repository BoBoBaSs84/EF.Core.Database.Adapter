using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Documents.Base;

namespace Application.Contracts.Requests.Documents;

/// <summary>
/// The request for updating a document.
/// </summary>
public sealed class DocumentUpdateRequest : DocumentBaseRequest
{
	/// <summary>
	/// The unique identifier of the document.
	/// </summary>
	[Required]
	public required Guid Id { get; init; }
}
