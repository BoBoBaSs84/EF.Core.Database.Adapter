using System.ComponentModel.DataAnnotations;

namespace BaseTests.Attributes;

/// <summary>
/// The test order attribute class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="TestPropertyAttribute"/> class.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TestOrderAttribute : TestPropertyAttribute
{
	public TestOrderAttribute([Range(1, int.MaxValue)] int order) : base("ExecutionOrder", $"{order}")
	{
	}
}
