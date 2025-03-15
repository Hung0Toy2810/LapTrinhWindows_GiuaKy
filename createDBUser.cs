            // //Up
            // // Bật chế độ Contained Database
            // migrationBuilder.Sql(@"
            //     IF EXISTS (SELECT 1 FROM sys.databases WHERE name = DB_NAME() AND containment = 0)
            //     BEGIN
            //         ALTER DATABASE CURRENT SET CONTAINMENT = PARTIAL;
            //     END
            // ", suppressTransaction: true);

            // // Tạo tài khoản AdminUser
            // migrationBuilder.Sql(@"
            //     IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'AdminUser')
            //     BEGIN
            //         CREATE USER AdminUser WITH PASSWORD = 'Admin@123StrongPass';
            //         ALTER ROLE db_owner ADD MEMBER AdminUser;
            //     END
            // ");

            // // Tạo tài khoản LibrarianUser
            // migrationBuilder.Sql(@"
            //     IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'LibrarianUser')
            //     BEGIN
            //         CREATE USER LibrarianUser WITH PASSWORD = 'Librarian@456Secure';
            //         GRANT SELECT, INSERT ON Books TO LibrarianUser;
            //         GRANT SELECT, INSERT ON Categories TO LibrarianUser;
            //         GRANT SELECT, INSERT ON ParentCategories TO LibrarianUser;
            //         GRANT SELECT, INSERT ON Users TO LibrarianUser;
            //         GRANT SELECT, INSERT, UPDATE ON BorrowingTransactions TO LibrarianUser;
            //         GRANT SELECT, INSERT, UPDATE ON Reservations TO LibrarianUser;
            //         GRANT SELECT, INSERT ON TransactionHistories TO LibrarianUser;
            //         GRANT SELECT ON SystemConfigurations TO LibrarianUser;
            //     END
            // ");

            // // Tạo tài khoản AppUser
            // migrationBuilder.Sql(@"
            //     IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'AppUser')
            //     BEGIN
            //         CREATE USER AppUser WITH PASSWORD = 'User@789SafePass';
            //         GRANT SELECT, INSERT ON Users TO AppUser;
            //         GRANT SELECT ON Books TO AppUser;
            //         GRANT SELECT ON Categories TO AppUser;
            //         GRANT SELECT, INSERT, UPDATE ON Reservations TO AppUser;
            //         GRANT SELECT ON TransactionHistories TO AppUser;
            //         GRANT SELECT ON SystemConfigurations TO AppUser;
            //         GRANT SELECT ON BorrowingTransactions TO AppUser;
            //     END
            // ");
            // //Down
            // migrationBuilder.Sql("DROP USER IF EXISTS AppUser;");
            // migrationBuilder.Sql("DROP USER IF EXISTS LibrarianUser;");
            // migrationBuilder.Sql("DROP USER IF EXISTS AdminUser;");
