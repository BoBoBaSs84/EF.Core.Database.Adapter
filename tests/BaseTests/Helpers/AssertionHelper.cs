using FluentAssertions.Execution;

namespace BB84.Home.BaseTests.Helpers;

public static class AssertionHelper
{
	public static void AssertInScope(Action action)
	{
		using AssertionScope assertionScope = new();
		action?.Invoke();
	}
}
