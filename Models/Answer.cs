using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INeed.Models
{
    public class Answer
    {
        [Key]
        [Display(Name = "ID odpowiedzi: ")]
        public Guid AnswerId { get; set; }

        [Required]
        [Display(Name = "ID pytania: ")]
        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question? Question { get; set; }

        [Display(Name = "Treść odpowiedzi: ")]
        [StringLength(30, ErrorMessage = "Treść odpowiedzi nie może być dłuższa niż 30 znaków.")]
        public string Reply { get; set; } = string.Empty;

        [Display(Name = "Wartość punktowa odpowiedzi: ")]
        public int Score { get; set; } = 0;
    }
}
