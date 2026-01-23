using System;
using System.ComponentModel.DataAnnotations;

namespace INeed.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        [Display(Name = "Nazwa kategorii (EN)")]
        public string? NameEN { get; set; }

        [Display(Name = "Kod")]
        public string Code { get; set; }

        [StringLength(10)]
        [Display(Name = "Kolor (HEX)")]
        public string Color { get; set; }

        // Normy (ciągi liczb oddzielone przecinkami)
        [Display(Name = "Normy (Kobiety)")]
        public string StenNormsFemale { get; set; }

        [Display(Name = "Normy (Mężczyźni)")]
        public string StenNormsMale { get; set; }

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