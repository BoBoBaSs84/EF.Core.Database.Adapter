using Application.Errors.Base;

using BB84.Extensions;

namespace Application.Errors.Services;

/// <summary>
/// The document service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the document service.
/// </remarks>
public static class DocumentServiceErrors
{
	private const string ErrorPrefix = $"{nameof(DocumentServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError CreateDocumentFailed(string document)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateDocumentFailed)}",
			$"{string.Empty.FormatInvariant(document)}");

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError CreateMultipleDocumentFailed(IEnumerable<string> documents)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{nameof(CreateDocumentFailed)}",
			$"{string.Empty.FormatInvariant(string.Join(',', documents))}");

	/// <summary>
	/// Error that indicates an exception during the todo service.
	/// </summary>
	public static ApiError CreateMultipleDocumentNotEmpty
		=> ApiError.CreateBadRequest($"{ErrorPrefix}.{nameof(CreateMultipleDocumentNotEmpty)}",
			$"{string.Empty}");
}
