namespace AntiqueOnlineShop.Domain.Exceptions;

public class InvalidProductModificationTimeException(Product product, DateTime modificationTime)
    : ArgumentException($"The product modification time {modificationTime} is not correct for product '{product.ProductName}' (ID: {product.Id}). It cannot be earlier than its creation time.", nameof(modificationTime))
{
    public Product Product => product;
    public DateTime ModificationTime => modificationTime;
}