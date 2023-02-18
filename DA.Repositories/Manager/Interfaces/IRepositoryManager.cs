using DA.Repositories.BaseTypes.Interfaces;
using Database.Adapter.Infrastructure.Contexts;

namespace DA.Repositories.Manager.Interfaces;

/// <summary>
/// The master repository interface.
/// </summary>
public partial interface IRepositoryManager : IUnitOfWork<ApplicationContext>
{
}
