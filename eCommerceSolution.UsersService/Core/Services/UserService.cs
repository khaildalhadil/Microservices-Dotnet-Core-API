using eCommerce.Core.DTO;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

public class UserService(IUserRepository repository) : IUserService
{

    public async Task<AuthenticationResponse?> Login(LoginRequest? loginRequest)
    {
        var user = await repository.GetUserByEmailAndPassword(loginRequest?.Email, loginRequest?.Password);
        
        if (user == null) {
            return null;
        }

        return new AuthenticationResponse
        {
            UserID = user.UserID,
            Email = user.Email,
            PersonName = user.PersonName,
            Gender = user.Gender,
            Success = true,
            Token = "token_token"
        };
    }

    public Task<AuthenticationResponse?> Register(RegisterRequest? registerRequest)
    {
        throw new NotImplementedException();
    }
}
