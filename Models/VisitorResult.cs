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
        [StringLength(50)]
        public string VisitorId { get; set; }

        public int FormId { get; set; } // <--- ZMIANA NA INT

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public virtual ICollection<VisitorCategoryScore> CategoryScores { get; set; } = new List<VisitorCategoryScore>();
    }

    // Klasa VisitorCategoryScore pozostaje bez zmian (ma Guid Id i Guid CategoryId)
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

        public int Score { get; set; }
        public int MaxScore { get; set; }
        public int Sten { get; set; }
    }
}