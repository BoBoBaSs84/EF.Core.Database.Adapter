namespace Domain.Enumerators;

/// <summary>
/// The work day type flags.
/// </summary>
[Flags]
public enum WorkDayTypes
{
	/// <summary>
	/// Indicates Sunday as a work day.
	/// </summary>
	Sunday = 1 << 0,
	/// <summary>
	/// Indicates Monday as a work day.
	/// </summary>
	Monday = 1 << 1,
	/// <summary>
	/// Indicates Tuesday as a work day.
	/// </summary>
	Tuesday = 1 << 2,
	/// <summary>
	/// Indicates Wednesday as a work day.
	/// </summary>
	Wednesday = 1 << 3,
	/// <summary>
	/// Indicates Thursday as a work day.
	/// </summary>
	Thursday = 1 << 4,
	/// <summary>
	/// Indicates Friday as a work day.
	/// </summary>
	Friday = 1 << 5,
	/// <summary>
	/// Indicates Saturday as a work day.
	/// </summary>
	Saturday = 1 << 6
}
