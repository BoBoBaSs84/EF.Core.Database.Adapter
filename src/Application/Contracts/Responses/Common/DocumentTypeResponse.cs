using BB84.Home.Application.Contracts.Responses.Common.Base;
using BB84.Home.Domain.Enumerators.Documents;

namespace BB84.Home.Application.Contracts.Responses.Common;

/// <summary>
/// The document types response.
/// </summary>
/// <inheritdoc/>
public sealed class DocumentTypeResponse(DocumentTypes enumValue) : EnumeratorBaseResponse<DocumentTypes>(enumValue)
{ }
