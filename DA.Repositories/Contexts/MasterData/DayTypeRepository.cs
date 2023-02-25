using DA.Domain.Models.MasterData;
using DA.Repositories.BaseTypes;
using DA.Repositories.Contexts.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Contexts.MasterData;

/// <summary>
/// The day type repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="EnumeratorRepository{TEnum}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IDayTypeRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class DayTypeRepository : EnumeratorRepository<DayType>, IDayTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DayTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public DayTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
