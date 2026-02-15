using System.Net;

namespace BB84.Home.Presentation.Common;

/// <summary>
/// Class with some constants for web
/// </summary>
public static class PresentationConstants
{
	/// <summary>
	/// Class with some constatnts for auth related things
	/// </summary>
	public static class Authentication
	{
		/// <summary>
		/// The security scheme name
		/// </summary>
		public const string SecuritySchemeName = "Authorization";

		/// <summary>
		/// The bearer token name
		/// </summary>
		public const string Bearer = "Bearer";

		/// <summary>
		/// The bearer token format
		/// </summary>
		public const string BearerFormat = "JWT";
	}

	/// <summary>
	/// Class with some constants for http policies
	/// </summary>
	public static class Policies
	{
		/// <summary>
		/// Cors policy
		/// </summary>
		public const string Cors = "CorsPolicy";
	}

	/// <summary>
	/// Class with some constants for the Http headers
	/// </summary>
	public static class HttpHeaders
	{
		/// <summary>
		/// Http request header for api version
		/// </summary>
		public const string Version = "X-Version";

		/// <summary>
		/// Http response header for pagination
		/// </summary>
		public const string Pagination = "X-Pagination";
	}

	/// <summary>
	/// Class with some constants for swagger
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	public static class Swagger
	{
		/// <summary>
		/// Swagger option for syntax highlighting
		/// </summary>
		public const string SytaxHighlightKey = "syntaxHighlight";
	}

	/// <summary>
	/// Class with constants for ProblemDetails
	/// </summary>
	public static class ProblemDetailsTypes
	{
		/// <summary>
		/// Error type for <see cref="HttpStatusCode.InternalServerError"/>
		/// </summary>
		public const string Error500Type = @"https://tools.ietf.org/html/rfc7231#section-6.6.1";
	}
}
