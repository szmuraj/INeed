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

        public string Code { get; set; }

        // --- TO POLE MUSI TU BYĆ ---
        [StringLength(20)]
        public string Color { get; set; }
        // ---------------------------

        public string StenNormsFemale { get; set; }
        public string StenNormsMale { get; set; }
    }
}