﻿using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Todo;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

/// <summary>
/// The interface for the todo item repository.
/// </summary>
/// <inheritdoc/>
public interface IItemRepository : IIdentityRepository<ItemEntity>
{ }
