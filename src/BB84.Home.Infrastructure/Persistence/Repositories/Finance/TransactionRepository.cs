﻿using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Domain.Entities.Finance;

namespace BB84.Home.Infrastructure.Persistence.Repositories.Finance;

/// <summary>
/// The transaction repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class TransactionRepository(IDbContext dbContext) : IdentityRepository<TransactionEntity>(dbContext), ITransactionRepository
{ }
