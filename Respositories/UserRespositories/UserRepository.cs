using System.Data.SqlClient;
using System.Threading.Tasks;
using LapTrinhWindow.Models;
using Microsoft.Extensions.Configuration;

namespace LapTrinhWindow.Repositories.UserRepositories
{
    public static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }

    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;
        

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserConnection");
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users WHERE Username = @userName";
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
            }

            return null; // Trả về null nếu có lỗi hoặc không tìm thấy
        }

        public async Task<int> CountAllUsersAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Users";
                
                var result = await command.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
            }

            return 0; // Trả về 0 nếu có lỗi
        }


        public async Task<User?> UpdateUserAsync(User user)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                using var transaction = await connection.BeginTransactionAsync(); // Bắt đầu giao dịch
                using var command = connection.CreateCommand();

                command.Transaction = (SqlTransaction)transaction;
                command.CommandText = @"
                    UPDATE Users 
                    SET Username = @username, 
                        Password = @password, 
                        FullName = @fullname, 
                        Gender = @gender, 
                        PhoneNumber = @phonenumber, 
                        Email = @email, 
                        Violation = @violation, 
                        SignUpDate = @signupdate, 
                        Status = @status, 
                        MemberType = @membertype, 
                        ExpirationDate = @expirationdate, 
                        BorrowedBooks = @borrowedbooks 
                    WHERE UserId = @userid";

                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@fullname", user.FullName);
                command.Parameters.AddWithValue("@gender", user.Gender.ToString());
                command.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@violation", user.Violation);
                command.Parameters.AddWithValue("@signupdate", user.SignUpDate);
                command.Parameters.AddWithValue("@status", user.Status.ToString());
                command.Parameters.AddWithValue("@membertype", user.MemberType.ToString());
                command.Parameters.AddWithValue("@expirationdate", user.ExpirationDate);
                command.Parameters.AddWithValue("@borrowedbooks", user.BorrowedBooks);
                command.Parameters.AddWithValue("@userid", user.UserId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    throw new Exception("Không tìm thấy người dùng để cập nhật.");
                }

                await transaction.CommitAsync(); // Xác nhận giao dịch
                return user;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
            }

            return null; // Trả về null nếu có lỗi
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = new List<User>();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users";

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    users.Add(new User
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
                        BorrowedBooks = reader.GetInt32(reader.GetOrdinal("BorrowedBooks")),
                    });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
            }

            return users; // Trả về danh sách dù có lỗi hay không (nếu lỗi xảy ra, danh sách rỗng)
        }
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users WHERE UserId = @userId";
                command.Parameters.AddWithValue("@userId", userId);

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
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                return null;
            }
        }
        public async Task<User> CreateUserAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync(); // Bắt đầu giao dịch
            try
            {
                using var command = connection.CreateCommand();
                command.Transaction = (SqlTransaction)transaction;
                command.CommandText = @"
                    INSERT INTO Users (Username, Password, FullName, Gender, PhoneNumber, Email, Violation, 
                    SignUpDate, Status, MemberType, ExpirationDate, BorrowedBooks) 
                    VALUES (@username, @password, @fullname, @gender, @phonenumber, @email, 
                    @violation, @signupdate, @status, @membertype, @expirationdate, @borrowedbooks);
                    SELECT SCOPE_IDENTITY();"; // Lấy ID của User vừa tạo

                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@fullname", user.FullName);
                command.Parameters.AddWithValue("@gender", user.Gender.ToString());
                command.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@violation", user.Violation);
                command.Parameters.AddWithValue("@signupdate", user.SignUpDate);
                command.Parameters.AddWithValue("@status", user.Status.ToString());
                command.Parameters.AddWithValue("@membertype", user.MemberType.ToString());
                command.Parameters.AddWithValue("@expirationdate", user.ExpirationDate);
                command.Parameters.AddWithValue("@borrowedbooks", user.BorrowedBooks);

                object result = await command.ExecuteScalarAsync() ?? 0; // Lấy UserId mới
                if (result != null)
                {
                    user.UserId = Convert.ToInt32(result); // Cập nhật UserId vào đối tượng User
                }

                await transaction.CommitAsync(); // Xác nhận giao dịch

                return user;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); // Hoàn tác giao dịch nếu có lỗi
                throw; // Ném lại lỗi để caller xử lý
            }
        }
        public async Task<IEnumerable<BookReservationDto>> GetReservedBooksByUserIdAsync(int userId) 
        {
            var results = new List<BookReservationDto>();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT 
                        b.BookId AS BookId,
                        b.BookName,
                        b.ISBN,
                        b.Author,
                        b.Publisher,
                        b.Pages,
                        b.Quantity,
                        b.Available,
                        b.CategoryId,
                        b.Price,
                        b.Place,
                        r.ReservationId,
                        r.UserId,
                        r.EmployeeId,
                        r.ReservedDate,
                        r.DueDate,
                        r.ExpirationDate,
                        r.Status
                    FROM Reservations r
                    JOIN Books b ON r.BookId = b.BookId
                    WHERE r.UserId = @userId;";

                command.Parameters.AddWithValue("@userId", userId);

                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    results.Add(new BookReservationDto
                    {
                        BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                        BookName = reader.GetString(reader.GetOrdinal("BookName")),
                        ISBN = reader.GetString(reader.GetOrdinal("ISBN")),
                        Author = reader.GetString(reader.GetOrdinal("Author")),
                        Publisher = reader.GetString(reader.GetOrdinal("Publisher")),
                        Pages = reader.GetInt32(reader.GetOrdinal("Pages")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                        Available = reader.GetInt32(reader.GetOrdinal("Available")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        Place = reader.GetString(reader.GetOrdinal("Place")),
                        ReservationId = reader.GetInt32(reader.GetOrdinal("ReservationId")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                        EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                        ReservedDate = reader.GetDateTime(reader.GetOrdinal("ReservedDate")),
                        DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                        ExpirationDate = reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                        Status = Enum.Parse<ReservationStatus>(reader.GetString(reader.GetOrdinal("Status")))
                    });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định: {ex.Message}");
            }

            return results;
        }
    }
}