using FluentAssertions.Execution;

namespace DA.BaseTests.Helpers;

public static class AssertionHelper
{
	public static void AssertInScope(Action action)
	{
		using AssertionScope assertionScope = new();
		action?.Invoke();
	}
}
