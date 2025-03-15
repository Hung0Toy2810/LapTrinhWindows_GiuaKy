using System.Collections.Generic;
using System.Threading.Tasks;
using LapTrinhWindow.Models;

namespace LapTrinhWindow.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        
        private readonly string? _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserConnection");
        }
       
        public async Task<User> GetUserByUserName(string userName)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE UserName = @userName";
            command.Parameters.AddWithValue("@userName", userName);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    FullName = reader.GetString(3),
                    
                };
            }
            return null;
        }
        
        
    }
}