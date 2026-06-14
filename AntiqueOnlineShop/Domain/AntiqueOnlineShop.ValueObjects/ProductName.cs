using AntiqueOnlineShop.ValueObjects.Base;
using AntiqueOnlineShop.ValueObjects.Validators;

namespace AntiqueOnlineShop.ValueObjects;


/// <param name="productName">Product's name.</param>
public class ProductName : ValueObject<string>
{
  public ProductName(string productName)
: base(new ProductNameValidator(), productName)
    {
    }
}
