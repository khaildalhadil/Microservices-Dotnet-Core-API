using eCommerce.Core.DTO;
using eCommerce.Core.Mappers;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<AuthenticationResponse?> Login(LoginRequest? loginRequest)
    {
        if (loginRequest is null) return null;

        var user = await repository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        return user?.ToAuthenticationResponse("token_token");
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest? registerRequest)
    {
        if (registerRequest is null) return null;

        var newUser = await repository.AddUser(registerRequest.ToEntity());

        return newUser?.ToAuthenticationResponse("token_token");
    }
}
