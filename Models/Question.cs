using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INeed.Models
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public Guid FormId { get; set; }
        public int Number { get; set; }

        public string Query { get; set; }
        public string? QueryEN { get; set; } // <--- NOWE POLE

        public string Category { get; set; }

        public Form Form { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}