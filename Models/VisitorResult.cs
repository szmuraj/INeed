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
        public int VisitorId { get; set; } = 100000;

        public int FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }

        [Required]
        public bool IsMale { get; set; } = true;

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

        public int Score { get; set; }
    }
}