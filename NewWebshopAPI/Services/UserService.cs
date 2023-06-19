using NewWebshopAPI.DTOs.UserDTOs;

namespace NewWebshopAPI.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse> GetUserByIdAsync(int userId);
        Task<UserResponse> RegisterUserAsync(RegisterUser newUser);
        Task<UserResponse> UpdateAsync(int userId, UserRequest updateUser);
        Task<UserResponse> DeleteAsync(int userId);
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
            return users?.Select(user => MapUserToUserResponse(user)).ToList();
        }

        public async Task<UserResponse> GetUserByIdAsync(int userId)
        {
            User user = await _userRepository.GetById(userId);
            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> RegisterUserAsync(RegisterUser newUser)
        {
            User user = new()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Phone = newUser.Phone,
                Address = newUser.Address,
                City = newUser.City,
                Zip = newUser.Zip,
                Email = newUser.Email,
                Password = newUser.Password,
                Role = Role.User,
            };

            user = await _userRepository.Create(user);

            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse> UpdateAsync(int userId, UserRequest updateUser)
        {
            User user = new()
            {
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Phone = updateUser.Phone,
                Address = updateUser.Address,
                City = updateUser.City,
                Zip = updateUser.Zip,
                Email = updateUser.Email,
            };

            // optional password
            if (updateUser.Password != null && updateUser.Password != string.Empty)
            {
                user.Password = updateUser.Password;
            }

            user = await _userRepository.Update(userId, user);

            return MapUserToUserResponse(user);
        }
        public async Task<UserResponse> DeleteAsync(int userId)
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
    }
}
