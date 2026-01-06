using System;
using System.ComponentModel.DataAnnotations;

namespace INeed.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? NameEN { get; set; } // <--- NOWE POLE

        public string Code { get; set; }

        [StringLength(10)]
        public string Color { get; set; }

        public string StenNormsFemale { get; set; }
        public string StenNormsMale { get; set; }
    }
}