using DA.Repositories.Contexts.Timekeeping;
using DA.Repositories.Contexts.Timekeeping.Interfaces;

namespace DA.Repositories.Manager;

internal sealed partial class RepositoryManager
{
	private Lazy<IAttendanceRepository> lazyAttendanceRepository = default!;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository => lazyAttendanceRepository.Value;

	private void InitializeTimekeeping()
	{
		lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(DbContext));
	}
}
