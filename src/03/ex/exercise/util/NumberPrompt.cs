using System.Reflection.Metadata.Ecma335;

namespace exercise.util;
public class NumberPrompt
{
    public double PromptForNumber()
    {
        bool first_run = false;
        double result;
        string maybe_number;
        do
        {
            if (first_run) Console.WriteLine("Numeric input must be valid");
            first_run = true;
            Console.Write("number?: ");
            string? user_input = Console.ReadLine();
            maybe_number = user_input ?? "";
        } while (! Double.TryParse(maybe_number, out result));
        return result;
    }
}