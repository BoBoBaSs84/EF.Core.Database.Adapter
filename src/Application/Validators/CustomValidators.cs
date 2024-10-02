using System.ComponentModel.DataAnnotations;

using FluentValidation;
using FluentValidation.Validators;

namespace Application.Validators;

/// <summary>
/// The static class for custom validators.
/// </summary>
public static class CustomValidators
{
	/// <summary>
	/// The phone custom validator class.
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
}
