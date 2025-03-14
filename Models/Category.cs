

namespace LapTrinhWindow.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = null!;
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        [Required]
        public CategoryStatus Status { get; set; } = CategoryStatus.Active;
        [ForeignKey("ParentCategoryId")]
        public ParentCategory ParentCategory { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
    public enum CategoryStatus
    {
        Active,
        Inactive
    }
}