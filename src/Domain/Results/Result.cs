using System.Diagnostics.CodeAnalysis;

namespace Domain.Results;

[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
public static class Result
{
	public static Success Success { get; }

	public static VoidResult Void { get; }

	public static Created Created { get; }

	public static Deleted Deleted { get; }

	public static Updated Updated { get; }
}
