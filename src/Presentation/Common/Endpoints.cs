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
	/// The calendar day route.
	/// </summary>
	internal static class CalendarDay
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(CalendarDay);
		internal const string GetPagedByParameters = EmptySuffix;
	}

	/// <summary>
	/// The day type route.
	/// </summary>
	internal static class DayType
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(DayType);
		internal const string GetPagedByParameters = EmptySuffix;
		internal const string GetByName = "{name}";
		internal const string GetById = "{id:int}";
	}
}
