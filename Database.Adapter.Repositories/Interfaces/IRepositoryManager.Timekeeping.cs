using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Database.Adapter.Repositories.Contexts.Timekeeping.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

/// <summary>
/// The master repository interface.
/// </summary>
public partial interface IRepositoryManager : IUnitOfWork<ApplicationContext>
{
	IAttendanceRepository AttendanceRepository { get; }
}
