using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INeed.Models
{
    public class Answer
    {
        [Key]
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question? Question { get; set; }

        [Display(Name = "Treść odpowiedzi")]
        [StringLength(255)]
        public string Reply { get; set; } = string.Empty;

        public string? ReplyEN { get; set; }

        [Display(Name = "Punktacja")]
        public int Score { get; set; }

        [Display(Name = "Kolejność wyświetlania")]
        public int? Order { get; set; } = 0;

        [Display(Name = "Kolor przycisku (HEX)")]
        [StringLength(20)]
        public string Color { get; set; } = "#607D8B";
    }
}