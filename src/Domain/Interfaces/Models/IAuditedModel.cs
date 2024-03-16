using Domain.Interfaces.Models.Base;

namespace Domain.Interfaces.Models;

/// <summary>
/// The audited interface.
/// </summary>
public interface IAuditedModel : IAuditedModelBase<Guid, Guid?>
{ }
