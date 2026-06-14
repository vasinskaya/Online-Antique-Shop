namespace AntiqueOnlineShop.Domain.Exceptions;

public class ProductNotBelongSellerException(Product product, Seller seller)
    : InvalidOperationException($"The product '{product.ProductName}' (ID: {product.Id}) is not managed by seller '{seller.Username}'.")
{
    public Product Product => product;
    public Seller Seller => seller;
}