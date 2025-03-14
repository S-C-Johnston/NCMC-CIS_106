using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace exercise.test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        string input = "";
        bool result = exercise.util.NumberPrompt.IsValidNumericInput(input);
        Assert.AreEqual(false, result);
    }
}