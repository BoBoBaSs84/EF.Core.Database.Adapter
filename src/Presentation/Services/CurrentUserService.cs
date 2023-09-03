using System.Security.Claims;

using Application.Interfaces.Presentation.Services;

using Microsoft.AspNetCore.Http;

namespace Presentation.Services;
internal sealed class CurrentUserService : ICurrentUserService
{
	private readonly IHttpContextAccessor _contextAccessor;

	/// <summary>
	/// Initializes a instance of the <see cref="CurrentUserService"/> class.
	/// </summary>
	/// <param name="contextAccessor">The http context accessor.</param>
	public CurrentUserService(IHttpContextAccessor contextAccessor) =>
		_contextAccessor = contextAccessor;

	/// <inheritdoc/>
	public Guid UserId =>
		Guid.Parse(_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!);

	/// <inheritdoc/>
	public string UserName =>
		_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)!;
}
