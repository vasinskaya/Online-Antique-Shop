using AntiqueOnlineShop.ValueObjects.Base;
using AntiqueOnlineShop.ValueObjects.Exceptions;

namespace AntiqueOnlineShop.ValueObjects.Validators;

public class UsernameValidator : IValidator<string>
{
    /// <summary>
    /// Max length of a username.
    /// </summary>
    public const int MAX_LENGTH = 30;

    /// <summary>
    /// Min length of a username.
    /// </summary>
    public const int MIN_LENGTH = 3;

    /// <summary>
    /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters.
    /// </summary>
    /// <param name="value">A string containing username.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
    /// <exception cref="ArgumentLongValueException"></exception>
    /// <exception cref="ArgumentShortValueException"></exception>
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }
}