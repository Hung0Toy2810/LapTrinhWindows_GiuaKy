namespace LapTrinhWindows.Respositories.UserRespositories
{
    public interface IUserRepository
    {
        // get user by id
        Task<User> GetUserByIdAsync(int userId);
        // get user by email
        Task<User> GetUserByEmailAsync(string email);
        // get user by username
        Task<User> GetUserByUserNameAsync(string userName);
        // create user
        Task<User> CreateUserAsync(User user);
        // update user
        Task<User> UpdateUserAsync(User user);
        // delete user
        Task<User> DeleteUserAsync(User user);
        // get all users
        Task<IEnumerable<User>> GetAllUsersAsync();
        // get all books borrowed by user
        Task<IEnumerable<Book>> GetBooksBorrowedByUserAsync(int userId);
        // get all books returned by user
        Task<IEnumerable<Book>> GetBooksReturnedByUserAsync(int userId);
        // create reservation
        Task<Reservation> CreateReservationAsync(Reservation reservation);
        // get all reservations by user
        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId);
        // get all borrowing transactions by user
        Task<IEnumerable<BorrowingTransaction>> GetBorrowingTransactionsByUserAsync(int userId);
        // get all transaction histories by user
        Task<IEnumerable<TransactionHistory>> GetTransactionHistoriesByUserAsync(int userId);
        // get all users by member type
        Task<IEnumerable<User>> GetUsersByMemberTypeAsync(MemberType memberType);
        // get all users by account status
        Task<IEnumerable<User>> GetUsersByAccountStatusAsync(AccountStatus accountStatus);
        // get all users by violation
        Task<IEnumerable<User>> GetUsersByViolationAsync(int violation);
        //count all users
        // 
        

    }
}
