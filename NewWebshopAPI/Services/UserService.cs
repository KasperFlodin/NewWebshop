using NewWebshopAPI.Authorization;
using NewWebshopAPI.Database.Entities;
using NewWebshopAPI.DTOs.AuthenticateDTOs;
using NewWebshopAPI.Repositories;

namespace NewWebshopAPI.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse> GetUserByIdAsync(int userId);
        Task<UserResponse> GetUserByEmailAsync(string userEmail);
        Task<UserResponse> RegisterUserAsync(RegisterUser newUser);
        Task<UserResponse> UpdateUserByIdAsync(int userId, UserRequest updateUser);
        Task<UserResponse> DeleteUserByIdAsync(int userId);
        AuthenticateResponse? Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }
        public async Task<List<UserResponse>> GetAllAsync()
        {
            List<User> users = await _userRepository.GetAll();
            if (users is null)
            {
                throw new ArgumentNullException();
            }

            return users.Select(user => new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                Zip = user.Zip,
                City = user.City,
                Phone = user.Phone,
                Role = user.Role,
            }).ToList();
        }

        public async Task<UserResponse> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<UserResponse> GetUserByEmailAsync(string userEmail)
        {
            User user = await _userRepository.GetByEmail(userEmail);
            if (user != null) 
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<UserResponse> RegisterUserAsync(RegisterUser newUser)
        {
            

            var user = await _userRepository.Create(MapRegisterUserToUser(newUser));

            if (user is null)
            {
                throw new ArgumentNullException();
            }

            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> UpdateUserByIdAsync(int userId, UserRequest updateUser)
        {
            var user = await _userRepository.Update(userId, MapUserRequestToUser(updateUser));

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            // optional password
            //if (updateUser.Password != null && updateUser.Password != string.Empty)
            //{
            //    user.Password = updateUser.Password;
            //}

            return null;
        }
        public async Task<UserResponse> DeleteUserByIdAsync(int userId)
        {
            User user = await _userRepository.Delete(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }

            return null;
        }

        public static UserResponse MapUserToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
                City = user.City,
                Zip = user.Zip,
                Email = user.Email,
                Role = user.Role,
            };
        }

        private User MapRegisterUserToUser(RegisterUser userRegister)
        {
            return new User
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                Phone = userRegister.Phone,
                Address = userRegister.Address,
                City = userRegister.City,
                Zip = userRegister.Zip,
                Email = userRegister.Email,
                Password = userRegister.Password,
            };
        }
        private User MapUserRequestToUser(UserRequest userRequest)
        {
            return new User
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Phone = userRequest.Phone,
                Address = userRequest.Address,
                City = userRequest.City,
                Zip = userRequest.Zip,
                Email = userRequest.Email,
                Password = userRequest.Password,
                Role = userRequest.Role,
            };
        }

        private readonly IJwtUtils _jwtUtils;
        // Authentication

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Email = "test", Password = "test" }
        };

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public async Task<LoginResponse> AuthenticateUserAsync(LoginRequest login)
        {
            User user = await _userRepository.GetByEmail(login.Email);

            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                LoginResponse response = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }

    }
}
