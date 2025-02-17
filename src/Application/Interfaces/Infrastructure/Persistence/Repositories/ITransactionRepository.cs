﻿using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Finance;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The transaction repository interface.
/// </summary>
/// <inheritdoc/>
public interface ITransactionRepository : IIdentityRepository<TransactionEntity>
{ }
