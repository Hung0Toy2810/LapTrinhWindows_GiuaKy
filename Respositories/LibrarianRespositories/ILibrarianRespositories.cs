namespace LapTrinhWindow.Respositories.LibarianRespositories
{
    public interface ILibrarianRespositories
    {
        // get librarian by id
        Task<Employee> GetLibrarianByIdAsync(int librarianId);
        // get librarian by email
        Task<Employee> GetLibrarianByEmailAsync(string email);
        // get librarian by username
        Task<Employee> GetLibrarianByUserNameAsync(string userName);
        // create librarian
        Task<Employee> CreateLibrarianAsync(Employee librarian);
        // update librarian
        Task<Employee> UpdateLibrarianAsync(Employee librarian);
        // delete librarian
        Task<Employee> DeleteLibrarianAsync(Employee librarian);
        // get all librarians
        Task<IEnumerable<Employee>> GetAllLibrariansAsync();
        //get all books by categoryID
        Task<IEnumerable<Book>> GetBooksByCategoryIDAsync(int categoryID);
        //get all books by ParentCategoryID
        Task<IEnumerable<Book>> GetBooksByParentCategoryIDAsync(int parentCategoryID);
        //get all books by author
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
        //get all books by publisher
        Task<IEnumerable<Book>> GetBooksByPublisherAsync(string publisher);
        //count all books
        Task<int> CountAllBooksAsync();
        //count all books by categoryID
        Task<int> CountBooksByCategoryIDAsync(int categoryID);
        //count all books return today
        Task<int> CountBooksReturnTodayAsync();
        //count all books borrowed today
        Task<int> CountBooksBorrowedTodayAsync();
        //count all books reserved today
        Task<int> CountBooksReservedTodayAsync();

    }
}