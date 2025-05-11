using final.Models;

namespace final.Services;

/// <summary>
/// BookService is meant to be a source of truth data source of Books
/// </summary>
public static class BookService
{

    private static List<Book> bookCollection = new();

    // I must implement the following:
    // Create: Add, "Add a book to the collection"
    // Read: List, "List all books from the collection"
    // Read: Display, "Display information about a book by ID"
    // Update: Edit, "Change details about the book"
    // Delete: Remove, "Remove a book by ID"

    /// <summary>
    /// AddBookRecord does what it says on the tin, checking for duplicates.
    /// </summary>
    /// <param name="book"></param>
    /// <returns>false if the book already exists</returns>
    public static bool AddBookRecord(Book book)
    {
        if (CheckForBook(book))
        {
            return false;
        }
        bookCollection.Add(book);
        return true;
    }

    /// <summary>
    /// GetAll does what it says on the tin.
    /// </summary>
    public static List<Book> GetAll()
    {
        return bookCollection;
    }

    /// <summary>
    /// GetBookByIndex accepts a numeric ID and a callback exterior variable. It
    /// returns a bool, and the exterior value is modified if applicable.
    /// </summary>
    /// <returns>false if the book was not found</returns>
    public static bool GetBookByIndex(int inputID, out Book? book)
    {
        try
        {
            book = bookCollection[inputID];
        }
        catch (ArgumentOutOfRangeException)
        {
            book = null;
            return false;
        }
        return true;
    }

    /// <summary>
    /// CheckForBook does what it says on the tin
    /// </summary>
    /// <param name="book">Book whose existence is checked for</param>
    /// <returns>bool condition of if the book input exists in the collection</returns>
    private static bool CheckForBook(Book book)
    {
        return bookCollection.Exists( b => b == book);
    }

    /// <summary>
    /// ReplaceBookRecord does what it says on the tin. The whole object at the
    /// given index is replaced.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="book"></param>
    /// <returns></returns>
    public static bool ReplaceBookRecord(int index, Book book)
    {
        try
        {
           bookCollection[index] = book;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// RemoveSingleBookRecord takes a numeric ID and removes the corresponding
    /// record, if any.
    /// </summary>
    /// <returns>false if the book was not found</returns>
    public static bool RemoveSingleBookRecord(int remove_book_ID)
    {
        try
        {
            bookCollection.RemoveAt(remove_book_ID);
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
        return true;
    }

}