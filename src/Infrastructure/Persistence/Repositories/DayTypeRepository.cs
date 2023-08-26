using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Entities.Enumerator;

using Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The day type repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="GenericRepository{TEntity}"/> class
/// and implements the <see cref="IDayTypeRepository"/> interface.
/// </remarks>
internal sealed class DayTypeRepository : GenericRepository<DayType>, IDayTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DayTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public DayTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
