using DA.Infrastructure.Contexts;
using DA.Repositories.BaseTypes.Interfaces;

namespace DA.Repositories.Manager.Interfaces;

/// <summary>
/// The master repository interface.
/// </summary>
public partial interface IRepositoryManager : IUnitOfWork<ApplicationContext>
{
}
