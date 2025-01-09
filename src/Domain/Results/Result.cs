namespace Domain.Results;

/// <summary>
/// The result class.
/// </summary>
public static class Result
{
	/// <summary>
	/// The result <see cref="Success"/> property.
	/// </summary>
	public static Success Success { get; }

	/// <summary>
	/// The result <see cref="Void"/> property.
	/// </summary>
	public static VoidResult Void { get; }

	/// <summary>
	/// The result <see cref="Created"/> property.
	/// </summary>
	public static Created Created { get; }

	/// <summary>
	/// The result <see cref="Deleted"/> property.
	/// </summary>
	public static Deleted Deleted { get; }

	/// <summary>
	/// The result <see cref="Updated"/> property.
	/// </summary>
	public static Updated Updated { get; }
}
