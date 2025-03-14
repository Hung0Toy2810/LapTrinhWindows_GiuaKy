namespace LapTrinhWindow.Models
{
    public class TransactionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TransactionHistoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public DateTime BorrowedDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime? ReturnedDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Fine { get; set; } = 0;
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        [ForeignKey("BookId")]
        public Book Book { get; set; } = null!;
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;
    }
}