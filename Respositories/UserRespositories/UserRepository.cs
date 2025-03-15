using System.Data.SqlClient;
using System.Threading.Tasks;
using LapTrinhWindow.Models;
using Microsoft.Extensions.Configuration;

namespace LapTrinhWindow.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserConnection");
        }

        public async Task<User?> GetUserByUserName(string userName)
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
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Gender = Enum.Parse<Gender>(reader.GetString(reader.GetOrdinal("Gender"))),
                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Violation = reader.GetInt32(reader.GetOrdinal("Violation")),
                    SignUpDate = reader.GetDateTime(reader.GetOrdinal("SignUpDate")),
                    Status = Enum.Parse<AccountStatus>(reader.GetString(reader.GetOrdinal("Status"))),
                    MemberType = Enum.Parse<MemberType>(reader.GetString(reader.GetOrdinal("MemberType"))),
                    ExpirationDate = reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                    BorrowedBooks = reader.GetInt32(reader.GetOrdinal("BorrowedBooks"))
                };
            }
            return null;
        }
    }
}