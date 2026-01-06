using System.ComponentModel.DataAnnotations;

namespace INeed.Models
{
    public class Form
    {
        [Key]
        [Display(Name = "ID formularza: ")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Tytuł formularza: ")]
        [StringLength(50, ErrorMessage = "Tytuł formularza nie może być dłuższy niż 50 znaków.")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Tytuł formularza (EN): ")]
        [StringLength(50, ErrorMessage = "Tytuł formularza nie może być dłuższy niż 50 znaków.")]
        public string? TitleEN { get; set; }

        [Display(Name = "Krótki opis formularza: ")]
        [StringLength(100, ErrorMessage = "Krótki opis formularza nie może być dłuższy niż 100 znaków.")]
        public string DecryptionShort { get; set; } = string.Empty;

        [Display(Name = "Krótki opis formularza (EN): ")]
        [StringLength(100, ErrorMessage = "Krótki opis formularza nie może być dłuższy niż 100 znaków.")]
        public string DecryptionShortEN { get; set; } = string.Empty;

        [Display(Name = "Pełny opis formularza: ")]
        [StringLength(255, ErrorMessage = "Pełny opis formularza nie może być dłuższy niż 255 znaków.")]
        public string DecryptionLong { get; set; } = string.Empty;

        [Display(Name = "Pełny opis formularza (EN): ")]
        [StringLength(255, ErrorMessage = "Pełny opis formularza nie może być dłuższy niż 255 znaków.")]
        public string DecryptionLongEN { get; set; } = string.Empty;

        [Display(Name = "Grafika do formularza: ")]
        [StringLength(30, ErrorMessage = "Grafika do formularza nie może być dłuższa niż 30 znaków.")]
        public string Graphic { get; set; } = string.Empty;

        [Display(Name = "Czy formularz jest aktywny: ")]
        public bool IsActive { get; set; } = false;

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}