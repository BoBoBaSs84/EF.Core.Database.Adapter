using FluentValidation;
using FluentValidation.Validators;

using static Application.Validators.CustomValidators;

namespace Application.Extensions;

/// <summary>
/// The <see cref="PropertyValidator{T, TProperty}"/> extension class.
/// </summary>
public static class ValidatorExtensions
{
	/// <summary>
	/// Checks if the property is a valid phone number.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	/// <param name="ruleBuilder">The rule binder to extend.</param>
	public static IRuleBuilderOptions<T, string?> Phone<T>(this IRuleBuilder<T, string?> ruleBuilder)
		=> ruleBuilder.SetValidator(new PhoneValidator<T>());
}
