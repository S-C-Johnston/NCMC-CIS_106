using final.Models;
using final.Services;

namespace final.test;

[TestClass]
public class BookServiceTests
{
    Book string_book = new Book
    {
        Title = "string",
        Author = "string",
        Genre = "string"
    };
    Book hhg = new Book
    {
        Title = "HitchHiker's Guide",
        Author = "Douglas Adams",
        Genre = "Sci-Fi,Comedy"
    };
    public BookService CreateBookService()
    {
        return new BookService();
    }

    [TestMethod]
    public void Story_BookService_RefusesDuplicates()
    {
        //Arrange
        var myBookService = CreateBookService();
        (bool first_book_success, int first_book_index) =
        myBookService.AddBookRecord(string_book);
        //Act
        (bool second_book_success, int second_book_index) =
        myBookService.AddBookRecord(string_book);
        //Assert
        Assert.IsFalse(second_book_success);
    }

    /// <summary>
    /// Story_BookService_FailsPutOnBadIndex tests the correct behavior of the
    /// PUT verb target on the bookService. Indices outside the range of the
    /// collection should be rejected.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="expectation">bool detailing the expected equivalence</param>
    [TestMethod]
    [DataRow(- 1, false)]
    [DataRow(0, true)]
    [DataRow(42, false)]
    public void Story_BookService_FailsPutOnBadIndex(int index, bool expectation)
    {
        //Arrange
        var myBookService = CreateBookService();
        myBookService.AddBookRecord(string_book);
        //Act-Assert
        Assert.AreEqual(
            myBookService.ReplaceBookRecord(index, hhg),
            expectation);
    }

    /// <summary>
    /// Story_BookService_RejectsDuplicatePut - Given that the AddBookRecord
    /// rejects duplicate books on insertion, replacement should do the same.
    /// </summary>
    [TestMethod]
    public void Story_BookService_RejectsDuplicatePut()
    {
        //Arrange
        var myBookService = CreateBookService();
        myBookService.AddBookRecord(string_book);
        (bool hhg_success, int hhg_index) = myBookService.AddBookRecord(hhg);
        //Act
        bool replacement_success = myBookService.ReplaceBookRecord(hhg_index, string_book);
        //Assert
        Assert.IsFalse(replacement_success);
    }
}