namespace LapTrinhWindow.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(20)")]
        public AccountStatus Status { get; set; } = AccountStatus.Active;
        [Column(TypeName = "nvarchar(20)")]
        public  Roles Role { get; set; } = Roles.Libararian;
        public ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
        public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
        public ICollection<SystemConfiguration> SystemConfigurations { get; set; } = new List<SystemConfiguration>();
    }
    public enum Roles
    {
        Admin,
        Libararian,
        Manager

    }
}