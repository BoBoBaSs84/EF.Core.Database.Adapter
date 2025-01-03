﻿using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Documents;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;

/// <summary>
/// The interface for the document data entity.
/// </summary>
/// <inheritdoc/>
public interface IDocumentDataRepository : IIdentityRepository<DataEntity>
{ }
