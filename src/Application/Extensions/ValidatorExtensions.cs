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
	public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
		=> ruleBuilder.SetValidator(new PhoneValidator<T>());

	/// <summary>
	/// Checks if the property is a valid international bank account number.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	/// <param name="ruleBuilder">The rule binder to extend.</param>
	public static IRuleBuilderOptions<T, string> InternationalBankAccountNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
		=> ruleBuilder.SetValidator(new InternationalBankAccountNumberValidator<T>());

	/// <summary>
	/// Checks if the property is a valid permanent account number.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	/// <param name="ruleBuilder">The rule binder to extend.</param>
	public static IRuleBuilderOptions<T, string> PermanentAccountNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
		=> ruleBuilder.SetValidator(new PermanentAccountNumberValidator<T>());
}
