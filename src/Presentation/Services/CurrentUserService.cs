﻿using System.Security.Claims;

using BB84.Home.Application.Interfaces.Presentation.Services;

using Microsoft.AspNetCore.Http;

namespace BB84.Home.Presentation.Services;

/// <summary>
/// The current user service class.
/// </summary>
/// <param name="contextAccessor">The http context accessor.</param>
internal sealed class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
{
	private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

	/// <inheritdoc/>
	public Guid UserId =>
		Guid.Parse(_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!);

	/// <inheritdoc/>
	public string UserName =>
		_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)!;
}
