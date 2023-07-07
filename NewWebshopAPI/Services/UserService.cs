using NewWebshopAPI.Database.Entities;
using NewWebshopAPI.DTOs.UserDTOs;

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
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
