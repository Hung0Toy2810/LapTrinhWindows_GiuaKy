namespace LapTrinhWindow.Models
{
    public class BorrowingTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BorrowingTransactionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime BorrowedDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        [ForeignKey("BookId")]
        public Book Book { get; set; } = null!;
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;
        
    }
}