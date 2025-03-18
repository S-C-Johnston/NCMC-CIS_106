using System.Text.RegularExpressions;

namespace exercise.util
{
    public class NumberPrompt
    {
        public static double? NumericExtract(string input)
        {
            Regex numeric_regex = new Regex(@"[-.]?\d+[.]?\d*");
            Match numeric_match = numeric_regex.Match(input);
            return Double.TryParse(numeric_match.Value, out double result)
            ? result
            : null;
        }
    }
}