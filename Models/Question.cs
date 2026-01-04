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

        [Display(Name = "Treść pytania: ")]
        [StringLength(50, ErrorMessage = "Treść pytania nie może być dłuższa niż 50 znaków.")]
        public string Query { get; set; } = string.Empty;
    }
}
