using AntiqueOnlineShop.Domain.Base;
using AntiqueOnlineShop.Domain.Exceptions;
using AntiqueOnlineShop.ValueObjects;

namespace AntiqueOnlineShop.Domain
{
    public class Product : Entity<Guid>
    {
        public ProductName? ProductName { get; private set; } = null; // Название товара

        public Description ProductDescription { get; private set; } = default!; // Описание товара

        public decimal Price { get; private set; } // Цена товара

        public string Condition { get; private set; } = default!; // Состояние товара (new attribute)

        public DateTime CreationTime { get; } // Время выставления товара на продажу

        public DateTime? LastUpdateTime { get; private set; } = null; // Время последнего изменения

        public Seller Seller { get; } = default!; // Продавец, которому принадлежит товар

        public ProductStatus Status { get; private set; } // Текущий статус продукта
        public Guid? BuyerId { get; private set; }        // Идентификатор покупателя, если продукт продан (Nullable FK)
        public DateTime? SaleTime { get; private set; }   // Время продажи продукта (Nullable)

        protected Product()
        {
        }

        public Product(
            Seller seller,
            ProductName productName,
            Description productDescription,
            decimal price,
            string condition,
            DateTime creationTime)
            : this(Guid.NewGuid(), seller, productName, productDescription, price, condition, creationTime) { }

        protected Product(
            Guid id,
            Seller seller,
            ProductName productName,
            Description productDescription,
            decimal price,
            string condition,
            DateTime creationTime,
            DateTime? lastUpdateTime = null)
            : base(id)
        {
            Seller = seller ?? throw new ArgumentNullValueException(nameof(seller));
            ProductName = productName ?? throw new ArgumentNullValueException(nameof(productName));
            ProductDescription = productDescription ?? throw new ArgumentNullValueException(nameof(productDescription));
            Price = price;
            Condition = condition ?? throw new ArgumentNullValueException(nameof(condition));

            if (lastUpdateTime is not null && lastUpdateTime < creationTime)
                throw new InvalidProductModificationTimeException(this, lastUpdateTime.Value);

            CreationTime = creationTime;
            LastUpdateTime = lastUpdateTime;

        }

        // Методы для изменения атрибутов продукта
        public bool SetProductName(ProductName newName)
        {
            if (ProductName == newName)
                return false;
            ProductName = newName;
            return true;
        }

        public bool SetProductDescription(Description newDescription)
        {
            if (ProductDescription == null) throw new ArgumentNullValueException(nameof(newDescription));
            if (ProductDescription == newDescription)
                return false;
            ProductDescription = newDescription;
            return true;
        }

        public bool SetPrice(decimal newPrice)
        {
            if (newPrice < 0) throw new ArgumentOutOfRangeException(nameof(newPrice), "Product price cannot be negative.");
            if (Price == newPrice)
                return false;
            Price = newPrice;
            return true;
        }

        public bool SetCondition(string newCondition)
        {
            if (string.IsNullOrWhiteSpace(newCondition)) throw new ArgumentNullValueException(nameof(newCondition));
            if (Condition == newCondition)
                return false;
            Condition = newCondition;
            return true;
        }

        public bool SetLastUpdateTime(DateTime modificationTime)
        {
            if (CreationTime > modificationTime) throw new InvalidProductModificationTimeException(this, modificationTime);
            if (LastUpdateTime is not null && LastUpdateTime > modificationTime)
                throw new InvalidProductModificationTimeException(this, modificationTime); // Нельзя отматывать время назад

            if (LastUpdateTime == modificationTime)
                return false;
            LastUpdateTime = modificationTime;
            return true;
        }

        public void MarkAsSold(Buyer buyer)
        {
            if (buyer == null) throw new ArgumentNullValueException(nameof(buyer));

            if (Status != ProductStatus.Available)
                throw new InvalidOperationException($"Product '{ProductName}' (ID: {Id}) is not available for sale. Current status: {Status}.");

            Status = ProductStatus.Sold;       // Изменяем статус на "Продан"
            BuyerId = buyer.Id;                // Записываем ID покупателя
            SaleTime = DateTime.UtcNow;        // Фиксируем время продажи
            LastUpdateTime = DateTime.UtcNow;  // Обновляем время последнего изменения
        }

        public void SetStatus(ProductStatus newStatus)
        {
            if (Status == newStatus) return; // Если статус не изменился

            Status = newStatus;
            LastUpdateTime = DateTime.UtcNow; // Обновляем время последнего изменения
        }

    }
}