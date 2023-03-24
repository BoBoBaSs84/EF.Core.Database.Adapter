using Application.Interfaces.Infrastructure.Identity;
using System.Security.Claims;

namespace WebAPI.Services;

/// <summary>
/// The current user service class.
/// </summary>
public sealed class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _contextAccessor;

	/// <summary>
	/// Initializes a instance of the <see cref="CurrentUserService"/> class.
	/// </summary>
	/// <param name="contextAccessor">The http context accessor.</param>
	public CurrentUserService(IHttpContextAccessor contextAccessor) =>
		_contextAccessor = contextAccessor;

	/// <inheritdoc/>
	public int UserId =>
		int.Parse(_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!, CultureInfo.CurrentCulture);

	/// <inheritdoc/>
	public string UserName =>
		_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)!;

	/// <inheritdoc/>
	public string Email =>
		_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email)!;
}
