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

        public int Number { get; set; }

        [StringLength(255)]
        public string Query { get; set; } = string.Empty;

        [StringLength(50)]
        public string Category { get; set; } = "Ogólne";
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}