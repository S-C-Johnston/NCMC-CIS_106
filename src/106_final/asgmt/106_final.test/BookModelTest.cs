using final.Models;
namespace final.test;

[TestClass]
public class BookModelTests
{
        private Book? null_book = null;
        private Book filled_book = new Book{
            Title = "string",
            Author = "string",
            Genre = "string"
        };
        private Book another_book = new Book{
            Title = "string",
            Author = "string",
            Genre = "string"
        };
        private Book hhg = new Book{
            Title = "HitchHiker's Guide",
            Author = "Douglas Adams",
            Genre = "Sci-Fi,Comedy"
        };

    [TestMethod]
    public void BookEquality_Null_ReturnFalse()
    {
        Assert.AreNotEqual(null_book, filled_book);
    }

    [TestMethod]
    public void BookEquality_Plain_ReturnTrue()
    {
        Assert.AreEqual(filled_book,another_book);
    }

    [TestMethod]
    public void BookEquality_DiffBooks_ReturnFalse()
    {
        Assert.AreNotEqual(filled_book,hhg);
    }
}