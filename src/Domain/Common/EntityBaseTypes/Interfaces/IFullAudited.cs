namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The full audited interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAudited"/> interface</item>
/// <item>The <see cref="ISoftDeleteable"/> interface</item>
/// </list>
/// </remarks>
public interface IFullAudited : IAudited, ISoftDeleteable
{
}
