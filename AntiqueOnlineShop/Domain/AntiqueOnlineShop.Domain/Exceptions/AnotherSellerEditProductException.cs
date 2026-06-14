namespace AntiqueOnlineShop.Domain.Exceptions;

public class AnotherSellerEditProductException(Product product, Seller actingSeller)
    : InvalidOperationException($"Seller '{actingSeller.Username}' can't edit product '{product.ProductName}' (ID: {product.Id}) owned by seller '{product.Seller.Username}'.")
{
    public Product Product => product;
    public Seller ActingSeller => actingSeller;
}