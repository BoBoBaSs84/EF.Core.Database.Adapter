using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Domain.Entities.Documents;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The interface for the document extension entity.
/// </summary>
/// <inheritdoc/>
public interface IDocumentExtensionRepository : IIdentityRepository<ExtensionEntity>
{ }
