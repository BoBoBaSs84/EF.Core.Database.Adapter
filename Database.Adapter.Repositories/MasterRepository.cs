using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Context.Interfaces;

namespace Database.Adapter.Repositories.Context;

/// <summary>
/// The master repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UnitOfWork{TContext}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IMasterRepository"/> interface</item>
/// </list>
/// </remarks>
public sealed class MasterRepository : UnitOfWork<MasterContext>, IMasterRepository
{
}
