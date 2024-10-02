using System.ComponentModel.DataAnnotations;

using Domain.Common;

using FluentValidation;
using FluentValidation.Validators;

namespace Application.Validators;

/// <summary>
/// The static class for custom validators.
/// </summary>
public static class CustomValidators
{
	/// <summary>
	/// The custom phone validator class.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	public sealed class PhoneValidator<T> : PropertyValidator<T, string?>
	{
		private static readonly PhoneAttribute PhoneAttribute = new();

		/// <inheritdoc/>
		public override string Name => "PhoneValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string? value)
			=> value is null || PhoneAttribute.IsValid(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
			=> "'{PropertyName}' is not a valid phone number.";
	}

	/// <summary>
	/// The custom international bank account number validator class.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	public sealed class InternationalBankAccountNumberValidator<T> : PropertyValidator<T, string>
	{
		/// <inheritdoc/>
		public override string Name => "InternationalBankAccountNumberValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string value)
			=> RegexStatics.IBAN.IsMatch(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
		 => "'{PropertyName}' is not a valid international bank account number.";
	}

	/// <summary>
	/// The custom permanent account number validator class.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	public sealed class PermanentAccountNumberValidator<T> : PropertyValidator<T, string>
	{
		/// <inheritdoc/>
		public override string Name => "PermanentAccountNumberValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string value)
			=> RegexStatics.PAN.IsMatch(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
			=> "'{PropertyName}' is not a valid permanent account number.";
	}
}
