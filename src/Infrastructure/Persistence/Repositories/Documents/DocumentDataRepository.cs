﻿using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Domain.Entities.Documents;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The document data repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class DocumentDataRepository(IDbContext dbContext) : IdentityRepository<DataEntity>(dbContext), IDocumentDataRepository
{ }
