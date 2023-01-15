using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

/// <summary>
/// The master repository interface.
/// </summary>
public interface IMasterRepository : IUnitOfWork<MasterContext>
{
}
