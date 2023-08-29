﻿namespace NewWebshopAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)

        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        //[Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllAsync();

                if (users.Count == 0)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]        // allow logged out users to access this endpoint
        [HttpPost("authenticate")]
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

        /*[Authorize(Role.Admin, Role.User)] */        // Only admins can access this endpoint
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            try
            {
                // only admins can access other user records
                //UserResponse currentUser = (UserResponse)HttpContext.Items["User"];

                //if (currentUser != null && userId != currentUser.Id && currentUser.Role != Role.Admin)
                //{
                //    return Unauthorized(new { message = "You are not authorized" });
                //}
                var user = await _userService.GetUserByIdAsync(userId);

                if (user is null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /*[Authorize(Role.Admin, Role.User)]*/         // Only admins can access this endpoint
        [HttpPut]
        [Route("{userId}")]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UserRequest updateUser)
        {
            try
            {
                var userResponse = await _userService.UpdateUserByIdAsync(userId, updateUser);

                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.Admin)]
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            try
            {
                //UserResponse currentUser = (UserResponse)HttpContext.Items["User"];

                //if (currentUser != null && userId != currentUser.Id && currentUser.Role != Role.Admin)
                //{
                //    return Unauthorized(new { message = "You are not authorized" });
                //}
                UserResponse user = await _userService.DeleteUserByIdAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
