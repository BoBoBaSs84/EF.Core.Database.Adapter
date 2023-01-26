using DA.Repositories.Contexts.Timekeeping.Interfaces;
using DA.Repositories.Contexts.Timekeeping;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private Lazy<IAttendanceRepository> lazyAttendanceRepository = default!;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository => lazyAttendanceRepository.Value;

	private void InitializeTimekeeping()
	{
		lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(DbContext));
	}
}
