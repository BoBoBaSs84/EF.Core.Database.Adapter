namespace Infrastructure.Common;

/// <summary>
/// The constants for the infrastructure layer.
/// </summary>
internal static class InfrastructureConstants
{
	/// <summary>
	/// The db function class.
	/// </summary>
	internal static class SqlDbFunction
	{
		/// <summary>
		/// The difference function.
		/// </summary>
		internal const string DIFFERENCE = "DIFFERENCE";

		/// <summary>
		/// The end of month function.
		/// </summary>
		internal const string ENDOFMONTH = "EOMONTH";

		/// <summary>
		/// The sound ex function.
		/// </summary>
		internal const string SOUNDEX = "SOUNDEX";

		/// <summary>
		/// The translate function.
		/// </summary>
		internal const string TRANSLATE = "TRANSLATE";

		/// <summary>
		/// The quote functions.
		/// </summary>
		internal const string QUOTENAME = "QUOTENAME";
	}

	/// <summary>
	/// The sql schema class.
	/// </summary>
	internal static class SqlSchema
	{
		/// <summary>
		/// The <see cref="Attendance"/> constant.
		/// </summary>
		internal const string Attendance = "attendance";

		/// <summary>
		/// The <see cref="Common"/> constant.
		/// </summary>
		internal const string Common = "common";

		/// <summary>
		/// The <see cref="Documents"/> constant.
		/// </summary>
		internal const string Documents = "documents";

		/// <summary>
		/// The <see cref="History"/> constant.
		/// </summary>
		internal const string History = "history";

		/// <summary>
		/// The <see cref="Identity"/> constant.
		/// </summary>
		internal const string Identity = "identity";

		/// <summary>
		/// The <see cref="Private"/> constant.
		/// </summary>
		internal const string Private = "private";

		/// <summary>
		/// The <see cref="Finance"/> constant.
		/// </summary>
		internal const string Finance = "finance";

		/// <summary>
		/// The <see cref="Todo"/> constant.
		/// </summary>
		internal const string Todo = "todo";
	}
}
