namespace LapTrinhWindow.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public Gender Gender { get; set; } = Gender.Female;
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Range (0, int.MaxValue, ErrorMessage = "Violation must be at least 0.")]
        public int Violation { get; set; } = 0;
        [Required]
        public DateTime SignUpDate { get; set; } = DateTime.Now;
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public AccountStatus Status { get; set; } = AccountStatus.Active;
        
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public MemberType MemberType { get; set; } = MemberType.Regular;
        [Required]
        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddYears(1);

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Borrowed books must be at least 0.")]
        public int BorrowedBooks { get; set; } = 0;
        
        public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
    }

    public enum AccountStatus
    {
        Active,
        Inactive,
        Blocked
    }

    public enum MemberType
    {
        Regular,
        Premium,
        VIP
    }
    public enum Gender
    {
        Male,
        Female
    }
}