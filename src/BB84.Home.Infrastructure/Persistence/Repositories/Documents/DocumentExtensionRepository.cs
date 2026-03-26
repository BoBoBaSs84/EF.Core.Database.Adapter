using BB84.EntityFrameworkCore.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// Represents the repository for the <see cref="ExtensionEntity"/> entity.
/// </summary>
internal sealed class DocumentExtensionRepository(IRepositoryContext repositoryContext)
	: IdentityRepository<ExtensionEntity>(repositoryContext), IDocumentExtensionRepository
{ }
