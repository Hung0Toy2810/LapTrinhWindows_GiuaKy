namespace LapTrinhWindow.Models
{
    public class ParentCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ParentCategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ParentCategoryName { get; set; } = null!;
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        [Required]
        public ParentCategoryStatus Status { get; set; } = ParentCategoryStatus.Active;
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
    public enum ParentCategoryStatus
    {
        Active,
        Inactive
    }
}