using FluentAssertions.Execution;

namespace BaseTests.Helpers;

public static class AssertionHelper
{
	public static void AssertInScope(Action action)
	{
		using AssertionScope assertionScope = new();
		action?.Invoke();
	}
}
