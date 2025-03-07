﻿namespace BB84.Home.Domain.Errors;

/// <summary>
/// A discriminated union of errors or a value.
/// </summary>
public readonly record struct ErrorOr<TValue> : IErrorOr
{
	private static readonly Error NoFirstError = Error.Unexpected(
		code: "ErrorOr.NoFirstError",
		description: "First error cannot be retrieved from a successful ErrorOr.");

	/// <summary>
	/// Initilizes an instance of <see cref="ErrorOr"/>.
	/// </summary>
	public ErrorOr()
	{ }

	private ErrorOr(Error error) => Errors = [error];

	private ErrorOr(List<Error> errors) => Errors = errors;

	private ErrorOr(TValue value) => Value = value;

	/// <summary>
	/// Creates an <see cref="ErrorOr{TValue}"/> from a list of errors.
	/// </summary>
	public static ErrorOr<TValue> From(List<Error> errors) => errors;

	/// <summary>
	/// Gets a value indicating whether the state is error.
	/// </summary>
	public bool IsError => Errors.Count > 0;

	/// <summary>
	/// Gets the list of errors.
	/// </summary>
	public List<Error> Errors { get; } = [];

	/// <summary>
	/// Gets the value.
	/// </summary>
	public TValue Value { get; } = default!;

	/// <summary>
	/// Gets the first error.
	/// </summary>
	public Error FirstError => !IsError ? NoFirstError : Errors![0];

	/// <summary>
	/// Creates an <see cref="ErrorOr{TValue}"/> from a value.
	/// </summary>
	public static implicit operator ErrorOr<TValue>(TValue value) => new(value);

	/// <summary>
	/// Creates an <see cref="ErrorOr{TValue}"/> from an error.
	/// </summary>
	/// <param name="error">A single error</param>
	public static implicit operator ErrorOr<TValue>(Error error) => new(error);

	/// <summary>
	/// Creates an <see cref="ErrorOr{TValue}"/> from a list of errors.
	/// </summary>
	/// <param name="errors">List of errors</param>
	public static implicit operator ErrorOr<TValue>(List<Error> errors) => new(errors);

	/// <summary>
	/// Creates an <see cref="ErrorOr{TValue}"/> from a list of errors.
	/// </summary>
	/// <param name="errors">List of errors</param>
	public static implicit operator ErrorOr<TValue>(Error[] errors) => new([.. errors]);

	/// <summary>
	/// Executes one of two actions, according to the result (Success/Failure)
	/// </summary>
	/// <param name="onValue">Action for success. Accepts the TValue as parameter</param>
	/// <param name="onError">Action for failure. Accepts the list of errors as parameter</param>
	public void Switch(Action<TValue?> onValue, Action<List<Error>> onError)
	{
		if (IsError)
		{
			onError?.Invoke(Errors);
			return;
		}

		onValue?.Invoke(Value);
	}

	/// <summary>
	/// Executes one of two actions, according to the result (Success/Failure)
	/// </summary>
	/// <param name="onValue">Action for success. Accepts the TValue as parameter</param>
	/// <param name="onFirstError">Action for failure. Accepts the first error as parameter</param>
	public void SwitchFirst(Action<TValue?> onValue, Action<Error> onFirstError)
	{
		if (IsError)
		{
			onFirstError?.Invoke(FirstError);
			return;
		}

		onValue?.Invoke(Value);
	}

	/// <summary>
	/// Executes one of two Func, according to the result (Success/Failure) and returs a TResult
	/// </summary>
	/// <typeparam name="TResult">Result type</typeparam>
	/// <param name="onValue">Func for success. Accepts the TValue as parameter</param>
	/// <param name="onError">Func for failure. Accepts the list of errors as parameter</param>
	/// <returns>TResult</returns>
	/// <exception cref="ArgumentNullException">If one of the func is null</exception>
	public TResult Match<TResult>(Func<TValue?, TResult> onValue, Func<List<Error>, TResult> onError)
	{
		_ = onValue ?? throw new ArgumentNullException(nameof(onValue));
		_ = onError ?? throw new ArgumentNullException(nameof(onError));

		return IsError ? onError(Errors) : onValue(Value);
	}

	/// <summary>
	/// Executes one of two Func, according to the result (Success/Failure) and returs a TResult
	/// </summary>
	/// <typeparam name="TResult">Result type</typeparam>
	/// <param name="onValue">Func for success. Accepts the TValue as parameter</param>
	/// <param name="onFirstError">Func for failure. Accepts the first error as parameter</param>
	/// <returns>TResult</returns>
	/// <exception cref="ArgumentNullException">If one of the func is null</exception>
	public TResult MatchFirst<TResult>(Func<TValue?, TResult> onValue, Func<Error, TResult> onFirstError)
	{
		_ = onValue ?? throw new ArgumentNullException(nameof(onValue));
		_ = onFirstError ?? throw new ArgumentNullException(nameof(onFirstError));

		return IsError ? onFirstError(FirstError) : onValue(Value);
	}
}

public static class ErrorOr
{
	public static ErrorOr<TValue> From<TValue>(TValue value) => value;
}
