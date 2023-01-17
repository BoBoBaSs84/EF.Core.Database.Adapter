using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

/// <summary>
/// The master repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IRepositoryManager"/> interface</item>
/// </list>
/// </remarks>
public sealed partial class RepositoryManager : UnitOfWork<ApplicationContext>, IRepositoryManager
{
	private readonly Lazy<IAttendanceRepository> lazyAttendanceRepository;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository => lazyAttendanceRepository.Value;
}
