using System.ComponentModel.DataAnnotations;

namespace BaseTests.Attributes;

/// <summary>
/// The test order attribute class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="TestPropertyAttribute"/> class.
/// </remarks>
/// <param name="order">The order number.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TestOrderAttribute([Range(1, int.MaxValue)] int order) : TestPropertyAttribute("ExecutionOrder", $"{order}")
{ }
