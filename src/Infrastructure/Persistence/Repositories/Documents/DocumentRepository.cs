using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Entities.Documents;

namespace Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The document repository class.
/// </summary>
/// <param name="repositoryContext">The repository context to use.</param>
internal sealed class DocumentRepository(IRepositoryContext repositoryContext) : IdentityRepository<DocumentEntity>(repositoryContext), IDocumentRepository
{ }
