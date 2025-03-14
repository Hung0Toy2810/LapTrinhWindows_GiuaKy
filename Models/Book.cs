namespace LapTrinhWindow.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BookId { get; set; }
        [Required]
        [MaxLength(100)]
        public string BookName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Publisher { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pages must be at least 1.")]
        public int Pages { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be at least 0.")]
        public int Quantity { get; set; } = 0;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available must be at least 0.")]
        public int Available { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be at least 0.")]
        public decimal Price { get; set; } = 0;
        [Required]
        [MaxLength(50)]
        public string Place { get; set; } = string.Empty;
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
        public ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
    }
}