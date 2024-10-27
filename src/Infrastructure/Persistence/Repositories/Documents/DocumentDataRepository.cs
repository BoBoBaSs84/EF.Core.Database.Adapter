using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Documents;

namespace Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The document data repository class.
/// </summary>
/// <param name="repositoryContext">The repository context to use.</param>
internal sealed class DocumentDataRepository(IRepositoryContext repositoryContext) : IdentityRepository<Data>(repositoryContext), IDocumentDataRepository
{ }
