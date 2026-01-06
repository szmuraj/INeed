using System.Collections.Generic;
using System.Linq;

namespace INeed.Helpers
{
    public enum StenCategory
    {
        Achievement,
        Affiliation,
        Autonomy,
        Dominance
    }

    public static class StenHelper
    {
        // Słownik: Kategoria -> (Płeć (false=K, true=M) -> Lista zakresów)
        private static readonly Dictionary<StenCategory, Dictionary<bool, List<(int Min, int Max)>>> Norms = new()
        {
            {
                StenCategory.Achievement, new Dictionary<bool, List<(int, int)>>
                {
                    { false, new List<(int, int)> { (5, 14), (15, 15), (16, 17), (18, 18), (19, 19), (20, 21), (22, 22), (23, 23), (24, 24), (25, 25) } },
                    { true,  new List<(int, int)> { (5, 12), (13, 15), (16, 17), (18, 18), (19, 19), (20, 21), (22, 22), (23, 23), (24, 24), (25, 25) } }
                }
            },
            {
                StenCategory.Affiliation, new Dictionary<bool, List<(int, int)>>
                {
                    { false, new List<(int, int)> { (4, 6), (7, 8), (9, 10), (11, 11), (12, 12), (13, 13), (14, 14), (15, 16), (17, 17), (18, 20) } },
                    { true,  new List<(int, int)> { (4, 6), (7, 8), (9, 9), (10, 10), (11, 11), (12, 13), (14, 14), (15, 15), (16, 18), (19, 20) } }
                }
            },
            {
                StenCategory.Autonomy, new Dictionary<bool, List<(int, int)>>
                {
                    { false, new List<(int, int)> { (5, 14), (15, 15), (16, 17), (18, 18), (19, 19), (20, 21), (22, 22), (23, 23), (24, 24), (25, 25) } },
                    { true,  new List<(int, int)> { (5, 13), (14, 16), (17, 17), (18, 18), (19, 20), (21, 21), (22, 22), (23, 23), (24, 24), (25, 25) } }
                }
            },
            {
                StenCategory.Dominance, new Dictionary<bool, List<(int, int)>>
                {
                    { false, new List<(int, int)> { (5, 7), (8, 9), (10, 11), (12, 13), (14, 14), (15, 16), (17, 18), (19, 21), (22, 22), (23, 25) } },
                    { true,  new List<(int, int)> { (5, 8), (9, 11), (12, 12), (13, 14), (15, 16), (17, 17), (18, 19), (20, 21), (22, 23), (24, 25) } }
                }
            }
        };

        public static int GetSten(StenCategory category, int score, bool isMale)
        {
            if (!Norms.ContainsKey(category)) return 0;

            var ranges = Norms[category][isMale];

            for (int i = 0; i < ranges.Count; i++)
            {
                if (score >= ranges[i].Min && score <= ranges[i].Max)
                {
                    return i + 1;
                }
            }

            if (ranges.Count > 0)
            {
                if (score < ranges[0].Min) return 1;
                if (score > ranges.Last().Max) return 10;
            }

            return 0;
        }

        public static string GetDescription(int sten)
        {
            if (sten >= 1 && sten <= 4) return "Niski";
            if (sten >= 5 && sten <= 6) return "Przeciętny";
            if (sten >= 7 && sten <= 10) return "Wysoki";
            return "-";
        }
    }
}