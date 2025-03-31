using FluentAssertions.Execution;

namespace BB84.Home.Base.Tests.Helpers;

public static class AssertionHelper
{
	public static void AssertInScope(Action action)
	{
		using AssertionScope assertionScope = new();
		action?.Invoke();
	}
}
