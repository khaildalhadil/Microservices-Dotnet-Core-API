using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mappers;

// Manual DTO <-> Entity mapping. This is the "no AutoMapper" way:
// plain methods, no reflection, no extra package. Easy to read and debug.
public static class UserMappingExtensions
{
    // RegisterRequest (incoming DTO) -> ApplicationUser (entity we store)
    public static ApplicationUser ToEntity(this RegisterRequest request) => new()
    {
        Email = request.Email,
        Password = request.Password,
        PersonName = request.PersonName,
        Gender = request.Gender.ToString(),
    };

    // ApplicationUser (entity) -> AuthenticationResponse (outgoing DTO)
    public static AuthenticationResponse ToAuthenticationResponse(this ApplicationUser user, string token) =>
        new(user.UserID, user.Email, user.PersonName, user.Gender, token, Success: true);
}
