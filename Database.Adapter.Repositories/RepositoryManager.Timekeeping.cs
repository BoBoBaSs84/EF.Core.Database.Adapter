using Database.Adapter.Repositories.Contexts.Timekeeping;
using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<IAttendanceRepository> lazyAttendanceRepository = default!;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository =>
		lazyAttendanceRepository.Value ?? new Lazy<IAttendanceRepository>(() => new AttendanceRepository(DbContext)).Value;
}
