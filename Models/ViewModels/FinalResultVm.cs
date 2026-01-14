using System.Collections.Generic;

namespace INeed.Models.ViewModels
{
    public class CategoryResultVm
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Color { get; set; }

        public int ScoreObtained { get; set; }
        public int ScoreMax { get; set; }

        // --- Pola dla wyników ---
        public int StenUser { get; set; }   // Wynik dla wybranej płci (lub 0 jeśli 'N')
        public int StenFemale { get; set; } // Wynik obliczony wg norm dla kobiet
        public int StenMale { get; set; }   // Wynik obliczony wg norm dla mężczyzn

        // --- Opisy STEN (Niski/Średni/Wysoki) ---
        public string DescFemale { get; set; }
        public string DescMale { get; set; }

        // --- Porady ---
        public string Advice { get; set; }       // Porada główna (dla wybranej płci)
        public string AdviceFemale { get; set; } // Porada specyficzna dla kobiet
        public string AdviceMale { get; set; }   // Porada specyficzna dla mężczyzn

        public double Percent => ScoreMax > 0 ? (double)ScoreObtained / ScoreMax * 100 : 0;
    }

    public class FinalResultVm
    {
        public string FormTitle { get; set; }
        public string VisitorId { get; set; }

        // Przekazujemy wybraną płeć: "F", "M" lub "N"
        public string Gender { get; set; }

        public List<CategoryResultVm> Categories { get; set; } = new List<CategoryResultVm>();
    }
}