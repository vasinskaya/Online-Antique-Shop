namespace AntiqueOnlineShop.Domain
{
    // Enum для определения текущего статуса продукта
    public enum ProductStatus
    {
        Available,  // Продукт доступен для продажи
        Sold,       // Продукт продан
        Removed,    // Продукт удален продавцом (снят с продажи)
        Pending     // Продукт в ожидании (например, на модерации или резерве)
    }
}