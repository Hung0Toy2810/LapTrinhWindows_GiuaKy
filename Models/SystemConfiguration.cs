using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapTrinhWindow.Models
{
    public class SystemConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ConfigID { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }
        
        [Required]
        public int MaxBorrowingPeriod { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal FineRate { get; set; } = 0;
        
        [Required]
        public int MaxBooksBorrowed { get; set; } = 0;
        
        [Required]
        public int MaxReservations { get; set; } = 0;
        
        [Required]
        public int ViolationPointsPerDayLate { get; set; } = 0; //điểm vi phạm mỗi ngày trễ
        
        [Required]
        public int ViolationPointsPerDamagedBook { get; set; } = 0; // điểm vi phạm mỗi quyển sách bị hỏng
        [Required]
        public int PointToBlock { get; set; } = 0; //điểm vi phạm để bị khóa tài khoản
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = null!;
    }
}