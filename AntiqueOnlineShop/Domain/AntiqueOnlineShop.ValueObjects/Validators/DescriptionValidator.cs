using AntiqueOnlineShop.ValueObjects.Base;
using AntiqueOnlineShop.ValueObjects.Exceptions; 

namespace AntiqueOnlineShop.ValueObjects.Validators;

public class DescriptionValidator : IValidator<string>
{
    /// <summary>
    /// The Description's max length
    /// </summary>
    public const int MAX_LENGTH = 2000;


    /// <summary>
    /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters.
    /// </summary>
    /// <param name="value">A string containing product's description.</param>
    /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
    /// <exception cref="ArgumentLongValueException"></exception>
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);

    }
}