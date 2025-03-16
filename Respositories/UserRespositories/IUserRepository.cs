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
        //Get all reserved books by userId
        Task<IEnumerable<BookReservationDto>> GetReservedBooksByUserIdAsync(int userId);
        // count all reserved books by userId
        Task<int> CountReservedBooksByUserIdAsync(int userId);
        // get all brrowed books by userId
        Task<IEnumerable<BorrowingTransactionBookDto>> GetBorrowedBooksByUserIdAsync(int userId);
        // get all book returned by userId
        Task<IEnumerable<TransactionHistoryBookDto>> GetTransactionHistoriesAsync(int userId);
        // count all book
        Task<int> CountAllBooksAsync();
        // count all book by categoryID
        Task<int> CountBooksByCategoryIDAsync(int categoryID);
        // count all book borrowed today by userId
        Task<int> CountBooksBorrowingAsync(int userId);
        // count all book reserved  by userId
        Task<int> CountBooksReservedAsync(int userId);
        // create book reservation
        Task<Reservation> CreateBookReservationAsync(Reservation bookReservation);
    }
}