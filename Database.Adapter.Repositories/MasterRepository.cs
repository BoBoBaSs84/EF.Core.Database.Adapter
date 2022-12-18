using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Interfaces;

namespace Database.Adapter.Repositories;

public sealed class MasterRepository : UnitOfWork<MasterContext>, IMasterRepository
{
}
