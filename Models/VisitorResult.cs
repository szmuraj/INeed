using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INeed.Models
{
    public class VisitorResult
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "ID odwiedzającego")]
        [StringLength(50)]
        public string VisitorId { get; set; }

        public int FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }

        // ZMIANA: bool? zamiast string.
        // true = Mężczyzna, false = Kobieta, null = Brak wyboru/Oba
        public bool? IsMale { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public virtual ICollection<VisitorCategoryScore> CategoryScores { get; set; } = new List<VisitorCategoryScore>();
    }

    public class VisitorCategoryScore
    {
        [Key]
        public Guid Id { get; set; }

        public Guid VisitorResultId { get; set; }

        [ForeignKey("VisitorResultId")]
        public virtual VisitorResult VisitorResult { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int Score { get; set; } // Tylko surowy wynik zostaje w bazie

        // USUNIĘTO: MaxScore i Sten (będą liczone w locie)
    }
}