using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INeed.Models
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }

        public int FormId { get; set; } // <--- ZMIANA NA INT
        public int Number { get; set; }

        public string Query { get; set; }
        public string? QueryEN { get; set; }

        public string Category { get; set; }

        [ForeignKey("FormId")]
        public Form Form { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}