using BB84.Home.Application.Features.Requests.Base;

namespace BB84.Home.Application.Features.Requests;

/// <summary>
/// The parameters for the document request.
/// </summary>
public sealed class DocumentParameters : RequestParameters
{
	/// <summary>
	/// The name of the document to filter the search by.
	/// </summary>
	public string? Name { get; init; }

	/// <summary>
	/// The extenion name of the document to filter the search by.
	/// </summary>
	public string? ExtensionName { get; init; }

	/// <summary>
	/// The directory of the document to filter the search by.
	/// </summary>
	public string? Directory { get; init; }
}
