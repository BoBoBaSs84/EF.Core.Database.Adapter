using DA.Repositories.Contexts.Timekeeping.Interfaces;

namespace DA.Repositories.Manager.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>
	/// The <see cref="AttendanceRepository"/> interface.
	/// </summary>
	IAttendanceRepository AttendanceRepository { get; }
}
