using System.Globalization;
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
            Regex numeric_regex = new Regex(@"[+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*)(?:[eE][+-]?\d+)?");
            // https://regexr.com/33jqd
            // In case of linkrot, this is based on the floats that javascript
            // understands, which seems to closely match the c# number format.
            // There are a number of exceptions. Credit to the user
            // 'StealthOfKing'
            //
            // In
            // https://learn.microsoft.com/en-us/dotnet/api/system.double.parse?view=net-9.0#system-double-parse(system-string)
            // remarks, M$ discusses the format expected by tryParse, which
            // amounts to:
            // [ws][sign][integral-digits[,]]integral-digits[.[fractional-digits]][E[sign]exponential-digits][ws]
            // This led me down a huge rabbit-hole of looking at i18n, which is
            // an involved topic. I think I have a handle on how the icu project
            // works, sort of, though I haven't read c header files in a long
            // time, so I'm not sure. I'm sure there's a relatively cheap way to
            // build a parser regex based on the current culture, given that
            // dotnet uses the icu library and provides culture and number
            // formatting locales. But, that is all wildly out of scope for this
            // exercise.
            //
            // Ultimately, this bit is an exercise in YAGNI. The requirements do
            // not demand permissive input or massaging. I could have just
            // written Double.TryParse() off the rip, and scolded the user for
            // input which did not conform to expectations. Double.Parse()
            // provides FormatExceptions, but this class hasn't yet covered
            // exceptions. Whatever. I'm leaving it in for now.
            Match numeric_match = numeric_regex.Match(input);
            return Double.TryParse(numeric_match.Value, out double result)
            ? result
            : null;
        }
    }
}