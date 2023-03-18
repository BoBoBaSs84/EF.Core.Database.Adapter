namespace Presentation.Common;

/// <summary>
/// Static class for the api endpoints.
/// </summary>
internal static class Endpoints
{
	private const string SiteRoot = "api";

	internal const string BaseUri = SiteRoot;

	internal const string EmptySuffix = "";

	/// <summary>
	/// The authentication route.
	/// </summary>
	internal static class Authentication
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Authentication);
		internal const string CreateUser = EmptySuffix;
		internal const string UpdateUser = "{userName}";
		internal const string AuthenticateUser = "Login";
	}

	/// <summary>
	/// The calendar route.
	/// </summary>
	internal static class Calendar
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Calendar);
		internal const string GetAll = EmptySuffix;
	}
}
