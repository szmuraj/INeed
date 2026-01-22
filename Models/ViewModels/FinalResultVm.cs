namespace INeed.Models.ViewModels
{
    public class CategoryResultVm
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Color { get; set; }

        public int ScoreObtained { get; set; }
        public int ScoreMax { get; set; }

        public int StenUser { get; set; }
        public int StenFemale { get; set; }
        public int StenMale { get; set; }

        public string DescFemale { get; set; }
        public string DescMale { get; set; }

        public string Advice { get; set; }
        public string AdviceFemale { get; set; }
        public string AdviceMale { get; set; }

        public double Percent => ScoreMax > 0 ? (double)ScoreObtained / ScoreMax * 100 : 0;
    }

    public class FinalResultVm
    {
        public int FormId { get; set; }

        public string FormTitle { get; set; }
        public string VisitorId { get; set; }
        public bool? IsMale { get; set; } // true = Mężczyzna, false = Kobieta, null = Brak wyboru

        public List<CategoryResultVm> Categories { get; set; } = new List<CategoryResultVm>();
    }
}