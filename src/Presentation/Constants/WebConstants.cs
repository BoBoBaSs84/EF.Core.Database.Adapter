using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Presentation.Constants;

/// <summary>
/// Class with some constants for web
/// </summary>
[SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Constants")]
public static class WebConstants
{
	/// <summary>
	/// Class with some constants for http policies
	/// </summary>
	public static class Policies
	{
		/// <summary>
		/// WKC cors policy
		/// </summary>
		public const string Cors = "WkcCorsPolicy";
	}

	/// <summary>
	/// Class with some constants for the <see cref="HttpContext"/>
	/// </summary>
	public static class HttpContext
	{
		/// <summary>
		/// Key for TraceId
		/// </summary>
		public const string TraceIdKey = "TraceId";

		/// <summary>
		/// Key for the query string of a request
		/// </summary>
		public const string QueryStringKey = "QueryString";

		/// <summary>
		/// Key for the endpoint name (Controller) of the request
		/// </summary>
		public const string EndpointNameKey = "EndpointName";
	}

	/// <summary>
	/// Class with some constants for the Http headers
	/// </summary>
	public static class HttpHeaders
	{
		/// <summary>
		/// Content type for <see cref="ProblemDetails"/>
		/// </summary>
		public const string ProblemDetailsContentType = "application/problem+json";

		/// <summary>
		/// Content type for json
		/// </summary>
		public const string JsonContentType = "application/json";

		/// <summary>
		/// Http request header for api version
		/// </summary>
		public const string Version = "X-Version";
	}

	/// <summary>
	/// Class with constants for problem details
	/// </summary>
	public static class ProblemDetails
	{
		/// <summary>
		/// Validation type for problem details object
		/// </summary>
		public const string ValidationErrroTitle = "Error.Validation";
	}

	/// <summary>
	/// Class with some constants for Middlewares
	/// </summary>
	public static class Middleware
	{
		/// <summary>
		/// Validation logger
		/// </summary>
		public const string ValidationLogger = "WkcValidationLogger";
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
