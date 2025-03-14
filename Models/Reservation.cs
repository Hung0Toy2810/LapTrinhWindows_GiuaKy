

namespace LapTrinhWindow.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ReservationId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int BookId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        
        [Required]
        public DateTime ReservedDate { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public DateTime ExpirationDate { get; set; } // Thuộc tính mới: ngày hết hạn
        
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Waiting; 
        
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        [ForeignKey("BookId")]
        public Book Book { get; set; } = null!;
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;
    }

    public enum ReservationStatus
    {
        Waiting,
        PickedUp,
        Cancelled
    }
}