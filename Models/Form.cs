using System.ComponentModel.DataAnnotations;

namespace INeed.Models
{
    public class Form
    {
        [Key]
        [Display(Name = "ID formularza: ")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł formularza: ")]
        [StringLength(50)]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Tytuł formularza (EN): ")]
        [StringLength(50)]
        public string? TitleEN { get; set; }

        public string DecryptionShort { get; set; } = string.Empty;
        public string DecryptionShortEN { get; set; } = string.Empty;
        public string DecryptionLong { get; set; } = string.Empty;
        public string DecryptionLongEN { get; set; } = string.Empty;
        public string Graphic { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}