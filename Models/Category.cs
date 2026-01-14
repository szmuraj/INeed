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
        public string? NameEN { get; set; }

        public string Code { get; set; }

        [StringLength(10)]
        public string Color { get; set; }

        // Normy (ciągi liczb oddzielone przecinkami)
        public string StenNormsFemale { get; set; }
        public string StenNormsMale { get; set; }

        // --- BRAKUJĄCE POLA (DORADZTWO) ---
        [Display(Name = "Porada (Niski wynik)")]
        public string? AdviceLow { get; set; }

        [Display(Name = "Porada (Średni wynik)")]
        public string? AdviceAvg { get; set; }

        [Display(Name = "Porada (Wysoki wynik)")]
        public string? AdviceHigh { get; set; }

        [Display(Name = "Porada EN (Niski wynik)")]
        public string? AdviceLowEN { get; set; }

        [Display(Name = "Porada EN (Średni wynik)")]
        public string? AdviceAvgEN { get; set; }

        [Display(Name = "Porada EN (Wysoki wynik)")]
        public string? AdviceHighEN { get; set; }
    }
}