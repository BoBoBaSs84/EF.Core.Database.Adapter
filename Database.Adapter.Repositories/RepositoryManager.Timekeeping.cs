using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<IAttendanceRepository> lazyAttendanceRepository;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository => lazyAttendanceRepository.Value;
}
