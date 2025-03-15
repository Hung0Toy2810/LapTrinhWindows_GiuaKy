namespace LapTrinhWindow.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserNameAsync(string userName);
        //count all users
        Task<int> CountAllUsersAsync();
        //update user
        Task<User?> UpdateUserAsync(User user);
        //get all users
        Task<IEnumerable<User>> GetAllUsersAsync();
        //get user by id
        Task<User?> GetUserByIdAsync(int userId);
        //create user
        Task<User> CreateUserAsync(User user);
    }
}