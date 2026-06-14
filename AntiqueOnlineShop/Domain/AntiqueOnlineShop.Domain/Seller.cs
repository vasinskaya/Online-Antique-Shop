using AntiqueOnlineShop.Domain.Base;
using AntiqueOnlineShop.Domain.Exceptions;
using AntiqueOnlineShop.ValueObjects;

namespace AntiqueOnlineShop.Domain
{
    /// <summary>
    /// Представляет продавца антиквариата.
    /// </summary>
    public class Seller : Entity<Guid>
    {
        private readonly ICollection<Product> _products = [];

        /// <summary> 
        /// Имя пользователя продавца.
        /// </summary>
        public Username Username { get; private set; }

        /// <summary>
        /// Список товаров, которыми управляет продавец.
        /// </summary>
        public IReadOnlyCollection<Product> Products =>
            _products.ToList().AsReadOnly(); // Возвращаем только для чтения

        // Конструктор
        public Seller(Guid id, Username username) : base(id)
        {
            Username = username ?? throw new ArgumentNullValueException(nameof(username));
        }

        // Конструктор для ORM, если потребуется
        protected Seller() : base(Guid.NewGuid()) { }

        /// <summary> 
        /// Изменяет имя пользователя продавца.
        /// </summary>
        /// <param name="newUsername">Новое имя пользователя.</param>
        internal bool ChangeUsername(Username newUsername)
        {
            if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));

            if (Username == newUsername) return false; // Если имя не изменилось

            Username = newUsername;
            return true;
        }

        // Операция "Create product" из Use Case
        public Product CreateProduct(ProductName name, Description description, decimal price, string condition)
        {
            // Создаем новый продукт, связывая его с этим продавцом
            var product = new Product(this, name, description, price, condition, DateTime.UtcNow);
            _products.Add(product);
            return product;
        }

        // Операция "Edit product"
        public bool EditProduct(Product product, ProductName newName, Description newDescription, decimal newPrice, string newCondition)
        {
            // Проверяем, что товар принадлежит этому продавцу
            if (product.Seller != this)
                throw new AnotherSellerEditProductException(product, this);

            // Проверяем, что товар находится в нашей коллекции (для консистентности)
            if (!_products.Any(p => p.Id == product.Id))
                throw new ProductNotBelongSellerException(product, this);

            // Применяем изменения
            var isEdit = product.SetProductName(newName)
                         || product.SetProductDescription(newDescription)
                         || product.SetPrice(newPrice)
                         || product.SetCondition(newCondition);

            // Если были изменения, обновляем время модификации
            if (isEdit) product.SetLastUpdateTime(DateTime.UtcNow);

            return isEdit;
        }

        // Операция "Remove product" из Use Case
        public void RemoveProduct(Product product)
        {
            // Проверяем, что товар принадлежит этому продавцу
            if (product.Seller != this)
                throw new AnotherSellerRemoveProductException(product, this);

            // Проверяем, что товар находится в нашей коллекции
            if (!_products.Any(p => p.Id == product.Id))
                throw new ProductNotBelongSellerException(product, this);

            _products.Remove(product);
        }
    }
}