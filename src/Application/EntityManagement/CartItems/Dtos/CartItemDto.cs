namespace Application.EntityManagement.CartItems.Dtos;

public record CartItemDto(
    int ExternalId,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotalPrice);