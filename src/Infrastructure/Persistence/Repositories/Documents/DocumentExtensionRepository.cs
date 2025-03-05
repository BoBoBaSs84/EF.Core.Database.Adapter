using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The document extension repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class DocumentExtensionRepository(IDbContext dbContext) : IdentityRepository<ExtensionEntity>(dbContext), IDocumentExtensionRepository
{ }
