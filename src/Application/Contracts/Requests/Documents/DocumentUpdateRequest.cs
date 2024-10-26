using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Requests.Documents;

/// <summary>
/// The request to update a document.
/// </summary>
public sealed class DocumentUpdateRequest
{
	/// <summary>
	/// The unique identifier of the document.
	/// </summary>
	[Required]
	public required Guid Id { get; init; }
}
