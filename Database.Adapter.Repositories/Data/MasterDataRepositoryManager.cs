﻿using Database.Adapter.Entities.MasterData;
using Database.Adapter.Infrastructure;
using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Database.Adapter.Repositories.Data.Interfaces;

namespace Database.Adapter.Repositories.Data;

/// <summary>
/// The master data repository manager class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interface <see cref="IMasterDataRepositoryManager"/>.
/// </remarks>
public class MasterDataRepositoryManager : UnitOfWork<MasterDataContext>, IMasterDataRepositoryManager
{
	private readonly Lazy<IGenericRepository<Calendar>> lazyCalendarRepository;

	/// <summary>
	/// The master data repository manager standard constructor.
	/// </summary>
	public MasterDataRepositoryManager()
	{
		lazyCalendarRepository = new Lazy<IGenericRepository<Calendar>>(() => new GenericRepository<Calendar>(dbContext));
	}

	/// <inheritdoc/>
	public IGenericRepository<Calendar> CalendarRepository => lazyCalendarRepository.Value;
}
