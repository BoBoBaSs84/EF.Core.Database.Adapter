using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Entities.Documents;

namespace Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The document extension repository class.
/// </summary>
/// <param name="repositoryContext">The repository context to use.</param>
internal sealed class DocumentExtensionRepository(IRepositoryContext repositoryContext) : IdentityRepository<ExtensionEntity>(repositoryContext), IDocumentExtensionRepository
{ }
