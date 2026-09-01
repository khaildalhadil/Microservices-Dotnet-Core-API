using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

/// <summary>
/// Authentication endpoints: user registration and login.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<LoginRequest> _loginValidator;

    public AuthController(
        IUserService userService,
        IValidator<RegisterRequest> registerValidator,
        IValidator<LoginRequest> loginValidator)
    {
        _userService = userService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="registerRequest">Email, password, person name and gender.</param>
    /// <returns>The authenticated user with a token on success.</returns>
    /// <response code="200">User registered successfully.</response>
    /// <response code="400">Validation failed or the user could not be created.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var validation = await _registerValidator.ValidateAsync(registerRequest);
        if (!validation.IsValid)
            return BadRequest(validation.ToDictionary());

        var response = await _userService.Register(registerRequest);
        if (response is null || !response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    /// <summary>
    /// Authenticates a user and returns a token.
    /// </summary>
    /// <param name="loginRequest">Email and password.</param>
    /// <returns>The authenticated user with a token on success.</returns>
    /// <response code="200">Login succeeded.</response>
    /// <response code="400">Validation failed.</response>
    /// <response code="401">Invalid email or password.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var validation = await _loginValidator.ValidateAsync(loginRequest);
        if (!validation.IsValid)
            return BadRequest(validation.ToDictionary());

        var response = await _userService.Login(loginRequest);
        if (response is null || !response.Success)
            return Unauthorized(response);

        return Ok(response);
    }
}
