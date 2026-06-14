using AntiqueOnlineShop.Domain.Base;
using AntiqueOnlineShop.Domain.Exceptions;
using AntiqueOnlineShop.ValueObjects;

namespace AntiqueOnlineShop.Domain
{
    /// <summary>
    /// Представляет покупателя в системе.
    /// </summary>
    public class Buyer : Entity<Guid>
    {
        /// <summary> 
        /// Имя пользователя покупателя.
        /// </summary>
        public Username Username { get; private set; }

        private readonly ICollection<Username> _user = [];


        // Конструктор
        public Buyer(Guid id, Username username) : base(id)
        {
            Username = username ?? throw new ArgumentNullValueException(nameof(username));
        }

        protected Buyer() : base(Guid.NewGuid()) { }

        public void BuyProduct(Product product)
        {
            if (product == null) throw new ArgumentNullValueException(nameof(product));

            if (product.Status != ProductStatus.Available)
            {
                throw new InvalidOperationException($"Product '{product.ProductName}' (ID: {product.Id}) is not available for purchase. Current status: {product.Status}.");
            }
            product.MarkAsSold(this);
        }

        /// <summary> 
        /// Изменяет имя пользователя покупателя.
        /// </summary>
        /// <param name="newUsername">Новое имя пользователя.</param>
        internal bool ChangeUsername(Username newUsername)
        {
            if (newUsername == null) throw new ArgumentNullValueException(nameof(newUsername));

            if (Username == newUsername) return false;

            Username = newUsername;
            return true;
        }

    }
}