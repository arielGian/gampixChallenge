using gampix.api.Domain;

namespace gampix.api.Services
{
    public class UserService : IUserService
    {
        // TODO: Implement with database context when ready
        private static List<User> _users = new();
        private static int _nextId = 1;

        public async Task<List<User>> GetAllUsersAsync()
        {
            await Task.Delay(0);
            return _users;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            await Task.Delay(0);
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            await Task.Delay(0);
            var user = new User
            {
                Id = _nextId++,
                Username = request.Username,
                Email = request.Email,
                Balance = request.InitialBalance
            };
            _users.Add(user);
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            await Task.Delay(0);
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found");

            if (!string.IsNullOrEmpty(request.Username))
                user.Username = request.Username;
            if (!string.IsNullOrEmpty(request.Email))
                user.Email = request.Email;
            if (request.Balance.HasValue)
                user.Balance = request.Balance.Value;

            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await Task.Delay(0);
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return false;

            _users.Remove(user);
            return true;
        }
    }
}
