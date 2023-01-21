using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>
	/// The <see cref="AttendanceRepository"/> interface.
	/// </summary>
	IAttendanceRepository AttendanceRepository { get; }
}
