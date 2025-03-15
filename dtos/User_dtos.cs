namespace LapTrinhWindow.dto
{
    public class UpdateUserByUserdto
    {
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

    public class BookReservationDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Pages { get; set; }
        public int Quantity { get; set; }
        public int Available { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Place { get; set; } = string.Empty;
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ReservedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ReservationStatus Status { get; set; }
    }
    public class BorrowingTransactionBookDto
    {
        // Thuộc tính của BorrowingTransaction
        public int BorrowingTransactionId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int Pages { get; set; }
        public int Quantity { get; set; }
        public int Available { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Place { get; set; } = string.Empty;

        
    }
}