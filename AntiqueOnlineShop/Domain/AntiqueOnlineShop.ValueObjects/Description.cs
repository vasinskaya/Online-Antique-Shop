using AntiqueOnlineShop.ValueObjects.Base;
using AntiqueOnlineShop.ValueObjects.Validators;

namespace AntiqueOnlineShop.ValueObjects;

/// <param name="description">Product's description.</param>
public class Description : ValueObject<string>
{
   public Description(string description)
        : base(new DescriptionValidator(), description)
    {
    }
}