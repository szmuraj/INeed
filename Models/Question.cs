using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INeed.Models
{
    public class Question
    {
        [Key]
        [Display(Name = "ID pytania: ")]
        public Guid QuestionId { get; set; }

        [Required]
        [Display(Name = "ID formularza: ")]
        public Guid FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual Form? Form { get; set; }

        // --- NOWE POLE: NUMER PYTANIA ---
        [Display(Name = "Numer pytania: ")]
        public int Number { get; set; }

        [Display(Name = "Treść pytania: ")]
        [StringLength(255, ErrorMessage = "Treść pytania nie może być dłuższa niż 255 znaków.")]
        public string Query { get; set; } = string.Empty;

        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}