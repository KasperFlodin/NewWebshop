using Microsoft.AspNetCore.Authorization;
using NewWebshopAPI.DTOs.UserDTOs;

namespace NewWebshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)

        {
            _userService = userService;
        }

        //[Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllAsync();

                if (users == null)
                {
                    return Problem("No data.");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[AllowAnonymous]        // allow logged out users to access this endpoint
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser newUser)
        {
            try
            {
                UserResponse user = await _userService.RegisterUserAsync(newUser);

                return Ok(user);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
