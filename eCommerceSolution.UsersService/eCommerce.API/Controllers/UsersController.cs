using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

/// <summary>
/// User lookup endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets a user by id.
    /// </summary>
    /// <param name="userID">The user's id.</param>
    /// <returns>The user's public details (no password).</returns>
    /// <response code="200">User found.</response>
    /// <response code="400">Empty user id.</response>
    /// <response code="404">No user with this id.</response>
    [HttpGet("{userID:guid}")]
    public async Task<ActionResult<UserDTO>> GetUserByUserID(Guid userID)
    {
        if (userID == Guid.Empty)
            return BadRequest("Invalid User ID");

        var response = await _userService.GetUserByUserID(userID);
        if (response is null)
            return NotFound();

        return Ok(response);
    }
}
