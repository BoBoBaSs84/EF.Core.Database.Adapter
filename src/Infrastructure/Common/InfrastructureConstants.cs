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
		/// The end of month function.
		/// </summary>
		internal const string ENDOFMONTH = "EOMONTH";

		/// <summary>
		/// The sound ex function.
		/// </summary>
		internal const string SOUNDLIKE = "SOUNDEX";

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
		internal const string Attendance = "Attendance";

		/// <summary>
		/// The <see cref="Common"/> constant.
		/// </summary>
		internal const string Common = "Common";

		/// <summary>
		/// The <see cref="History"/> constant.
		/// </summary>
		internal const string History = "History";

		/// <summary>
		/// The <see cref="Identity"/> constant.
		/// </summary>
		internal const string Identity = "Identity";

		/// <summary>
		/// The <see cref="Migration"/> constant.
		/// </summary>
		internal const string Migration = "Migration";

		/// <summary>
		/// The <see cref="Finance"/> constant.
		/// </summary>
		internal const string Finance = "Finance";

		/// <summary>
		/// The <see cref="Todo"/> constant.
		/// </summary>
		internal const string Todo = "Todo";
	}
}
