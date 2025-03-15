namespace LapTrinhWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDBContext>();
                    var userRepository = services.GetRequiredService<IUserRepository>();
                    Console.WriteLine("DbContext resolved successfully.");
                    TestUserRepository(userRepository).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                //SeedDatabase(services.GetRequiredService<ApplicationDBContext>());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();
                    string DefaultConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                    services.AddDbContext<ApplicationDBContext>(options =>
                        options.UseSqlServer("Server=localhost,1433;Database=LibraryManagement;User Id=sa;Password=YourPassword123;TrustServerCertificate=true;"));//thay connection string vào đây!!!
                    services.AddScoped<IUserRepository, UserRepository>();
                });
        
        private static async Task TestUserRepository(IUserRepository userRepository)
        {
            var user = await userRepository.GetUserByUserName("Username2");
            if (user != null)
            {
                Console.WriteLine($"User found: {user.FullName}");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        private static void SeedDatabase(ApplicationDBContext context)
        {
            if (!context.ParentCategories.Any())
            {
                var parentCategories = new[]
                {
                    new ParentCategory { ParentCategoryName = "Parent Category 1", Description = "Description 1", LastUpdated = DateTime.Now, Status = ParentCategoryStatus.Active },
                    new ParentCategory { ParentCategoryName = "Parent Category 2", Description = "Description 2", LastUpdated = DateTime.Now, Status = ParentCategoryStatus.Active },
                    new ParentCategory { ParentCategoryName = "Parent Category 3", Description = "Description 3", LastUpdated = DateTime.Now, Status = ParentCategoryStatus.Active },
                    new ParentCategory { ParentCategoryName = "Parent Category 4", Description = "Description 4", LastUpdated = DateTime.Now, Status = ParentCategoryStatus.Active },
                };

                context.ParentCategories.AddRange(parentCategories);
                context.SaveChanges();
                Console.WriteLine("4 parent categories have been added to the database.");
            }
            else
            {
                Console.WriteLine("Parent categories already exist in the database.");
            }
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { CategoryName = "Category 1", ParentCategoryId = 1, Description = "Description 1", LastUpdated = DateTime.Now, Status = CategoryStatus.Active },
                    new Category { CategoryName = "Category 2", ParentCategoryId = 2, Description = "Description 2", LastUpdated = DateTime.Now, Status = CategoryStatus.Active },
                    new Category { CategoryName = "Category 3", ParentCategoryId = 3, Description = "Description 3", LastUpdated = DateTime.Now, Status = CategoryStatus.Active },
                    new Category { CategoryName = "Category 4", ParentCategoryId = 4, Description = "Description 4", LastUpdated = DateTime.Now, Status = CategoryStatus.Active },
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
                Console.WriteLine("10 categories have been added to the database.");
            }
            else
            {
                Console.WriteLine("Categories already exist in the database.");
            }
            if (!context.Books.Any())
            {
                var books = new[]
                {
                    new Book { BookName = "Book 1", ISBN = "ISBN1", Author = "Author 1", Publisher = "Publisher 1", Pages = 100, Quantity = 10, Available = 10, CategoryId = 1, Price = 9.99m, Place = "Shelf 1" },
                    new Book { BookName = "Book 2", ISBN = "ISBN2", Author = "Author 2", Publisher = "Publisher 2", Pages = 200, Quantity = 20, Available = 20, CategoryId = 2, Price = 19.99m, Place = "Shelf 2" },
                    new Book { BookName = "Book 3", ISBN = "ISBN3", Author = "Author 3", Publisher = "Publisher 3", Pages = 300, Quantity = 30, Available = 30, CategoryId = 3, Price = 29.99m, Place = "Shelf 3" },
                    new Book { BookName = "Book 4", ISBN = "ISBN4", Author = "Author 4", Publisher = "Publisher 4", Pages = 400, Quantity = 40, Available = 40, CategoryId = 4, Price = 39.99m, Place = "Shelf 4" },
                    new Book { BookName = "Book 5", ISBN = "ISBN5", Author = "Author 5", Publisher = "Publisher 5", Pages = 500, Quantity = 50, Available = 50, CategoryId = 1, Price = 49.99m, Place = "Shelf 5" },
                    new Book { BookName = "Book 6", ISBN = "ISBN6", Author = "Author 6", Publisher = "Publisher 6", Pages = 600, Quantity = 60, Available = 60, CategoryId = 2, Price = 59.99m, Place = "Shelf 6" },
                    new Book { BookName = "Book 7", ISBN = "ISBN7", Author = "Author 7", Publisher = "Publisher 7", Pages = 700, Quantity = 70, Available = 70, CategoryId = 3, Price = 69.99m, Place = "Shelf 7" },
                    new Book { BookName = "Book 8", ISBN = "ISBN8", Author = "Author 8", Publisher = "Publisher 8", Pages = 800, Quantity = 80, Available = 80, CategoryId = 4, Price = 79.99m, Place = "Shelf 8" },
                    new Book { BookName = "Book 9", ISBN = "ISBN9", Author = "Author 9", Publisher = "Publisher 9", Pages = 900, Quantity = 90, Available = 90, CategoryId = 1, Price = 89.99m, Place = "Shelf 9" },
                    new Book { BookName = "Book 10", ISBN = "ISBN10", Author = "Author 10", Publisher = "Publisher 10", Pages = 1000, Quantity = 100, Available = 100, CategoryId = 2, Price = 99.99m, Place = "Shelf 10" }
                };

                context.Books.AddRange(books);
                context.SaveChanges();
                Console.WriteLine("10 books have been added to the database.");
            }
            else
            {
                Console.WriteLine("Books already exist in the database.");
            }


            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new User { FullName = "User 1",Username="Username1", Password = "Password1", Gender = Gender.Male, PhoneNumber = "1234567890", Email = "user1@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 2",Username="Username2", Password = "Password2", Gender = Gender.Female, PhoneNumber = "1234567891", Email = "user2@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 3",Username="Username3", Password = "Password3", Gender = Gender.Male, PhoneNumber = "1234567892", Email = "user3@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 4",Username="Username4", Password = "Password4", Gender = Gender.Female, PhoneNumber = "1234567893", Email = "user4@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 5",Username="Username5", Password = "Password5", Gender = Gender.Male, PhoneNumber = "1234567894", Email = "user5@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 6",Username="Username6", Password = "Password6", Gender = Gender.Female, PhoneNumber = "1234567895", Email = "user6@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 7",Username="Username7", Password = "Password7", Gender = Gender.Male, PhoneNumber = "1234567896", Email = "user7@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 8",Username="Username8", Password = "Password8", Gender = Gender.Female, PhoneNumber = "1234567897", Email = "user8@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 9",Username="Username9", Password = "Password9", Gender = Gender.Male, PhoneNumber = "1234567898", Email = "user9@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 },
                    new User { FullName = "User 10",Username="Username10", Password = "Password10", Gender = Gender.Female, PhoneNumber = "1234567899", Email = "user10@example.com", Violation = 0, SignUpDate = DateTime.Now, Status = AccountStatus.Active, MemberType = MemberType.Regular, ExpirationDate = DateTime.Now.AddYears(1), BorrowedBooks = 0 }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
                Console.WriteLine("10 users have been added to the database.");
            }
            else
            {
                Console.WriteLine("Users already exist in the database.");
            }

            if (!context.Employees.Any())
            {
                var employees = new[]
                {
                    new Employee { Username = "employee1", Password = "password1", FullName = "Employee 1", PhoneNumber = "1234567890", Email = "employee1@example.com", Status = AccountStatus.Active, Role = Roles.Libararian },
                    new Employee { Username = "employee2", Password = "password2", FullName = "Employee 2", PhoneNumber = "1234567891", Email = "employee2@example.com", Status = AccountStatus.Active, Role = Roles.Libararian },
                    new Employee { Username = "employee3", Password = "password3", FullName = "Employee 3", PhoneNumber = "1234567892", Email = "employee3@example.com", Status = AccountStatus.Active, Role = Roles.Libararian },
                    new Employee { Username = "employee4", Password = "password4", FullName = "Employee 4", PhoneNumber = "1234567893", Email = "employee4@example.com", Status = AccountStatus.Active, Role = Roles.Libararian },
                    new Employee { Username = "employee5", Password = "password5", FullName = "Employee 5", PhoneNumber = "1234567894", Email = "employee4@example.com", Status = AccountStatus.Active, Role = Roles.Admin },
                    new Employee { Username = "employee6", Password = "password6", FullName = "Employee 6", PhoneNumber = "1234567895", Email = "employee4@example.com", Status = AccountStatus.Active, Role = Roles.Admin },
                };

                context.Employees.AddRange(employees);
                context.SaveChanges();
                Console.WriteLine("10 employees have been added to the database.");
            }
            else
            {
                Console.WriteLine("Employees already exist in the database.");
            }

            if (!context.SystemConfigurations.Any())
            {
                var systemConfigurations = new[]
                {
                    new SystemConfiguration { EmployeeId = 1, MaxBorrowingPeriod = 14, FineRate = 0.5m, MaxBooksBorrowed = 5, MaxReservations = 3, ViolationPointsPerDayLate = 1, ViolationPointsPerDamagedBook = 5, PointToBlock = 10 },
                    new SystemConfiguration { EmployeeId = 2, MaxBorrowingPeriod = 14, FineRate = 0.5m, MaxBooksBorrowed = 5, MaxReservations = 3, ViolationPointsPerDayLate = 1, ViolationPointsPerDamagedBook = 5, PointToBlock = 10 },
                    new SystemConfiguration { EmployeeId = 3, MaxBorrowingPeriod = 14, FineRate = 0.5m, MaxBooksBorrowed = 5, MaxReservations = 3, ViolationPointsPerDayLate = 1, ViolationPointsPerDamagedBook = 5, PointToBlock = 10 },
                    new SystemConfiguration { EmployeeId = 4, MaxBorrowingPeriod = 14, FineRate = 0.5m, MaxBooksBorrowed = 5, MaxReservations = 3, ViolationPointsPerDayLate = 1, ViolationPointsPerDamagedBook = 5, PointToBlock = 10 }

                };

                context.SystemConfigurations.AddRange(systemConfigurations);
                context.SaveChanges();
                Console.WriteLine("4 system configurations have been added to the database.");
            }
            else
            {
                Console.WriteLine("System configurations already exist in the database.");
            }
            if (!context.Reservations.Any())
            {
                var reservations = new[]
                {
                    new Reservation { UserId = 1, BookId = 1, EmployeeId = 1, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 2, BookId = 2, EmployeeId = 2, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 3, BookId = 3, EmployeeId = 3, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 4, BookId = 4, EmployeeId = 4, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 5, BookId = 5, EmployeeId = 1, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 6, BookId = 6, EmployeeId = 2, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 7, BookId = 7, EmployeeId = 3, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 8, BookId = 8, EmployeeId = 3, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 9, BookId = 9, EmployeeId = 4, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting },
                    new Reservation { UserId = 10, BookId = 10, EmployeeId = 1, ReservedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ExpirationDate = DateTime.Now.AddDays(7), Status = ReservationStatus.Waiting }
                };

                context.Reservations.AddRange(reservations);
                context.SaveChanges();
                Console.WriteLine("10 reservations have been added to the database.");
            }
            else
            {
                Console.WriteLine("Reservations already exist in the database.");
            }

            if (!context.BorrowingTransactions.Any())
            {
                var borrowingTransactions = new[]
                {
                    new BorrowingTransaction { UserId = 1, BookId = 1, EmployeeId = 1, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 2, BookId = 2, EmployeeId = 2, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 3, BookId = 3, EmployeeId = 3, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 4, BookId = 4, EmployeeId = 4, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 5, BookId = 5, EmployeeId = 1, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 6, BookId = 6, EmployeeId = 2, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 7, BookId = 7, EmployeeId = 3, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 8, BookId = 8, EmployeeId = 4, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 9, BookId = 9, EmployeeId = 1, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) },
                    new BorrowingTransaction { UserId = 10, BookId = 10, EmployeeId = 3, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) }
                };

                context.BorrowingTransactions.AddRange(borrowingTransactions);
                context.SaveChanges();
                Console.WriteLine("10 borrowing transactions have been added to the database.");
            }
            else
            {
                Console.WriteLine("Borrowing transactions already exist in the database.");
            }

            if (!context.TransactionHistories.Any())
            {
                var transactionHistories = new[]
                {
                    new TransactionHistory { UserId = 1, BookId = 1, EmployeeId = 1, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 2, BookId = 2, EmployeeId = 2, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 3, BookId = 3, EmployeeId = 3, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 4, BookId = 4, EmployeeId = 4, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 5, BookId = 5, EmployeeId = 1, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 6, BookId = 6, EmployeeId = 2, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 7, BookId = 7, EmployeeId = 3, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 8, BookId = 8, EmployeeId = 4, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 9, BookId = 9, EmployeeId = 1, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 },
                    new TransactionHistory { UserId = 10, BookId = 10, EmployeeId = 2, TransactionDate = DateTime.Now, BorrowedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14), ReturnedDate = DateTime.Now.AddDays(14), Fine = 0 }
                };

                context.TransactionHistories.AddRange(transactionHistories);
                context.SaveChanges();
                Console.WriteLine("10 transaction histories have been added to the database.");
            }
            else
            {
                Console.WriteLine("Transaction histories already exist in the database.");
            }

        }
    
    }
}