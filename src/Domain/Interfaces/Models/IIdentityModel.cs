using Domain.Interfaces.Models.Base;

namespace Domain.Interfaces.Models;

/// <summary>
/// The identity model interface.
/// </summary>
public interface IIdentityModel : IIdentityModelBase<Guid>
{ }
