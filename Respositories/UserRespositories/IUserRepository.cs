

namespace LapTrinhWindow.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByUserName(string userName);
    }
}