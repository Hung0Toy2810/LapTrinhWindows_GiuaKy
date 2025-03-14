
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LapTrinhWindow.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<TransactionHistory> TransactionHistories { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<BorrowingTransaction> BorrowingTransactions { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<ParentCategory> ParentCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<BorrowingTransaction>().ToTable("BorrowingTransactions");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<ParentCategory>().ToTable("ParentCategories");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<SystemConfiguration>().ToTable("SystemConfigurations");
            modelBuilder.Entity<TransactionHistory>().ToTable("TransactionHistories");
            modelBuilder.Entity<User>().ToTable("Users");


            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasOne(th => th.Employee)
                    .WithMany(e => e.TransactionHistories)
                    .HasForeignKey(th => th.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(th => th.User)
                    .WithMany(u => u.TransactionHistories)
                    .HasForeignKey(th => th.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(th => th.Book)
                    .WithMany(b => b.TransactionHistories)
                    .HasForeignKey(th => th.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(th => th.UserId);
                entity.HasIndex(th => th.BookId);
                entity.HasIndex(th => th.EmployeeId);
            });

            modelBuilder.Entity<BorrowingTransaction>(entity =>
            {
                entity.HasOne(bt => bt.Employee)
                    .WithMany(e => e.BorrowingTransactions)
                    .HasForeignKey(bt => bt.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(bt => bt.User)
                    .WithMany(u => u.BorrowingTransactions)
                    .HasForeignKey(bt => bt.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(bt => bt.Book)
                    .WithMany(b => b.BorrowingTransactions)
                    .HasForeignKey(bt => bt.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(bt => bt.UserId);
                entity.HasIndex(bt => bt.BookId);
                entity.HasIndex(bt => bt.EmployeeId);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Book)
                    .WithMany(b => b.Reservations)
                    .HasForeignKey(r => r.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(r => r.UserId);
                entity.HasIndex(r => r.BookId);
                entity.Property(r => r.Status)
                    .HasConversion<string>();
            });
            modelBuilder.Entity<SystemConfiguration>(entity =>
            {
                entity.HasOne(sc => sc.Employee)
                    .WithMany(e => e.SystemConfigurations)
                    .HasForeignKey(sc => sc.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .IsUnique();
                entity.Property(e => e.Role)
                    .HasConversion<string>();
                entity.Property(e => e.Status)
                    .HasConversion<string>();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email)
                    .IsUnique();
                entity.Property(u => u.Gender)
                    .HasConversion<string>();
                entity.Property(u => u.Status)
                    .HasConversion<string>();
                entity.Property(u => u.MemberType)
                    .HasConversion<string>();
                entity.HasIndex(u => u.Username)
                    .IsUnique();
            });
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(b => b.ISBN)
                    .IsUnique();
                entity.HasOne(b => b.Category)
                    .WithMany(c => c.Books)
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(b => b.CategoryId);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.CategoryName)
                    .IsUnique();
                entity.Property(c => c.Status)
                    .HasConversion<string>();
                entity.HasOne(c => c.ParentCategory)
                    .WithMany(pc => pc.Categories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(c => c.ParentCategoryId);
            });
            modelBuilder.Entity<ParentCategory>(entity =>
            {
                entity.HasIndex(pc => pc.ParentCategoryName)
                    .IsUnique();
                entity.Property(pc => pc.Status)
                    .HasConversion<string>();
            });
            modelBuilder.HasAnnotation("SqlServer:ContainedDatabaseAuthentication", true);

            
            modelBuilder.HasAnnotation("SqlServer:Script", @"
                IF EXISTS (SELECT 1 FROM sys.databases WHERE name = DB_NAME() AND containment = 0)
                BEGIN
                    ALTER DATABASE CURRENT SET CONTAINMENT = PARTIAL;
                END
            ");

            
            modelBuilder.HasAnnotation("SqlServer:Script", @"
                IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'AdminUser')
                BEGIN
                    CREATE USER AdminUser WITH PASSWORD = 'Admin@123StrongPass';
                    ALTER ROLE db_owner ADD MEMBER AdminUser;
                END
            ");

            
            modelBuilder.HasAnnotation("SqlServer:Script", @"
                IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'LibrarianUser')
                BEGIN
                    CREATE USER LibrarianUser WITH PASSWORD = 'Librarian@456Secure';
                    GRANT SELECT ON Books TO LibrarianUser;
                    GRANT SELECT ON Categories TO LibrarianUser;
                    GRANT SELECT, INSERT, UPDATE, DELETE ON BorrowingTransactions TO LibrarianUser;
                    GRANT SELECT, INSERT, UPDATE, DELETE ON Reservation TO LibrarianUser;
                    GRANT SELECT, INSERT ON TransactionHistories TO LibrarianUser;
                END
            ");

            
            modelBuilder.HasAnnotation("SqlServer:Script", @"
                IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'AppUser')
                BEGIN
                    CREATE USER AppUser WITH PASSWORD = 'User@789SafePass';
                    GRANT SELECT ON Books TO AppUser;
                    GRANT SELECT ON Categories TO AppUser;
                    GRANT SELECT, INSERT, UPDATE, DELETE ON Reservation TO AppUser;
                END
            ");

        }
    }
}