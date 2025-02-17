﻿using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;

using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Documents;

namespace Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The document extension repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class DocumentExtensionRepository(IDbContext dbContext) : IdentityRepository<ExtensionEntity>(dbContext), IDocumentExtensionRepository
{ }
