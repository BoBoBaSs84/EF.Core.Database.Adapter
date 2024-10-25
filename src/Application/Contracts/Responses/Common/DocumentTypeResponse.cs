using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators.Documents;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The document types response.
/// </summary>
/// <inheritdoc/>
public sealed class DocumentTypeResponse(DocumentTypes enumValue) : EnumeratorBaseResponse<DocumentTypes>(enumValue)
{ }
