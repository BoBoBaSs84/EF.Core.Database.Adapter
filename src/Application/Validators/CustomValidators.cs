using System.ComponentModel.DataAnnotations;

using FluentValidation;
using FluentValidation.Validators;

using static BB84.Home.Application.Common.ApplicationStatics;

namespace BB84.Home.Application.Validators;

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
		public override string Name
			=> "PhoneValidator";

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
	public sealed class InternationalBankAccountNumberValidator<T> : PropertyValidator<T, string?>
	{
		/// <inheritdoc/>
		public override string Name
			=> "InternationalBankAccountNumberValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string? value)
			=> value is null || RegexStatics.IBAN.IsMatch(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
			=> "'{PropertyName}' is not a valid international bank account number.";
	}

	/// <summary>
	/// The custom permanent account number validator class.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	public sealed class PermanentAccountNumberValidator<T> : PropertyValidator<T, string?>
	{
		/// <inheritdoc/>
		public override string Name
			=> "PermanentAccountNumberValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string? value)
			=> value is null || RegexStatics.PAN.IsMatch(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
			=> "'{PropertyName}' is not a valid permanent account number.";
	}

	/// <summary>
	/// The custom bank identification code validator class.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	public sealed class BankIdentificationCodeValidator<T> : PropertyValidator<T, string?>
	{
		/// <inheritdoc/>
		public override string Name
			=> "BankIdentificationCodeValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string? value)
			=> value is null || RegexStatics.BIC.IsMatch(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
			=> "'{PropertyName}' is not a valid bank identification code.";
	}

	/// <summary>
	/// The custom rgb hex validator class.
	/// </summary>
	/// <typeparam name="T">The type to work with.</typeparam>
	public sealed class RgbHexValidator<T> : PropertyValidator<T, string?>
	{
		/// <inheritdoc/>
		public override string Name
			=> "RgbHexValidator";

		/// <inheritdoc/>
		public override bool IsValid(ValidationContext<T> context, string? value)
			=> value is null || RegexStatics.HEXRGB.IsMatch(value);

		/// <inheritdoc/>
		protected override string GetDefaultMessageTemplate(string errorCode)
			=> "'{PropertyName}' is not a valid rgb hex string representation.";
	}
}
