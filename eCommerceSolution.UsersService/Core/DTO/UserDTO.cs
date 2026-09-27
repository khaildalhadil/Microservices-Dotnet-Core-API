namespace eCommerce.Core.DTO;

// Public user details (no password) returned to other services / clients.
public record UserDTO(Guid UserID, string? Email, string? PersonName, string? Gender);
