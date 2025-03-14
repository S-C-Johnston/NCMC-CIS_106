namespace exercise.util
{
    public class NumberPrompt
    {
        public static bool IsValidNumericInput(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return true;
        }
    }
}