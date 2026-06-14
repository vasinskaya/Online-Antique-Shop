using AntiqueOnlineShop.ValueObjects.Base;
using AntiqueOnlineShop.ValueObjects.Validators;

namespace AntiqueOnlineShop.ValueObjects;

/// <summary>
/// Represents type of the entity's username.
/// </summary>
/// <param name="name">The username of the entity.</param>
public class Username(string name) : ValueObject<string>(new UsernameValidator(), name);