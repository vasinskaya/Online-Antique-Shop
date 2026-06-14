using AntiqueOnlineShop.Domain.Repositories.Abstractions.Base;

namespace AntiqueOnlineShop.Domain.Repositories.Abstractions;

public interface IBuyerRepository : IRepository<Buyer, Guid>
{
    // Так как имя пользователя уникальное
    Task<Buyer?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}
public interface ISellerRepository : IRepository<Seller, Guid>
{
    // Так как имя пользователя уникальное
    Task<Buyer?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}
