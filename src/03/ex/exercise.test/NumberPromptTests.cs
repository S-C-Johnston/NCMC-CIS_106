using exercise.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exercise.test;

[TestClass]
public class NumberPromptTests
{
    [TestMethod]
    public void EmptyStringIsNotNumericInput()
    {
        string input = "";
        double? result = NumberPrompt.NumericExtract(input);
        Assert.IsNull(result);
    }

    [TestMethod]
    public void StringsWithNumbersAreValid()
    {
        Dictionary<string,Tuple<string,double>> inputs = new()
        {
            {"Embedded_Negative", new Tuple<string,double>("asdf-10ghjk", -10)},
            {"Negative_Floating", new Tuple<string,double>("-3.14",-3.14)},
            {"Embedded_Floating", new Tuple<string,double>("sentence.42", .42)},
            {"Phone_Number", new Tuple<string,double>("1-800-555-1234", 1)}
        };

        foreach (KeyValuePair<string,Tuple<string,double>> input in inputs)
        {
            double? result = NumberPrompt.NumericExtract(input.Value.Item1);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
            Assert.AreEqual(input.Value.Item2, result.Value, 1E-14);
        };
    }
}