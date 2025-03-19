using System.Text.RegularExpressions;

namespace exercise.util
{
    public class NumberPrompt
    {
        /// <summary>
        /// NumericExtract uses a regular expression to parse an input string
        /// for that which looks like a number, and returns the first one, or
        /// null if not found.
        /// </summary>
        /// <param name="input">string: hopefully it has a number in it</param>
        /// <returns>the first number found as a double, or null if not. double?
        /// as a nullable type can be examined with $_.HasValue and
        /// $_.Value</returns>
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