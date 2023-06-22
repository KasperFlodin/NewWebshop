namespace NewWebshopAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task<User> Create(User newUser);
        Task<User> Update(int userId, User updateUser);
        Task<User> Delete(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetById(int userId)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> Create(User newUser)
        {
            //if (_dbContext.User.Any(u => u.Email == newUser.Email))
            //{
            //    throw new Exception("Email " + newUser.Email + " is not available");
            //}

            _dbContext.User.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> Update(int userId, User updateUser)
        {
            User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                if (_dbContext.User.Any(u => u.Id != userId && u.Email == user.Email))
                {
                    throw new Exception("Email " + user.Email + " does not exist");
                }

                // set required values
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Phone = updateUser.Phone;
                user.Address = updateUser.Address;
                user.City = updateUser.City;
                user.Zip = updateUser.Zip;
                user.Email = updateUser.Email;

                if (user.Password != null && user.Password != string.Empty)
                {
                    user.Password = updateUser.Password;
                }
            }

            return user;
        }

        public async Task<User> Delete(int userId)
        {
            User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                if (_dbContext.User.Any(u => u.Id != userId))
                {
                    throw new Exception("Email does not exist");
                }

                _dbContext.User.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }
    }
}
