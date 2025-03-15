namespace LapTrinhWindow.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserName(string userName);
    }
}