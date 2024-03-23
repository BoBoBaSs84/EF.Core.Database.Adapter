using Domain.Interfaces.Models.Base;

namespace Domain.Interfaces.Models;

/// <summary>
/// The enumerator model interface.
/// </summary>
public interface IEnumeratorModel : IIdentityModelBase<int>, IEnumeratorModelBase, ISoftDeleteableBase
{ }
