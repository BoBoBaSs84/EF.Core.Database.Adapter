﻿using System.Net;

using BB84.Home.Domain.Errors;

namespace BB84.Home.Application.Errors.Base;

/// <summary>
/// Api errors
/// </summary>
public static class ApiErrors
{
	/// <summary>
	/// Error that represents that multiple erros have occured
	/// </summary>
	public static readonly ApiError CompositeError =
		ApiError.Create(
			Error.Composite("Error.Composite"),
			HttpStatusCode.InternalServerError);
}

