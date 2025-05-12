using final.Data;
using final.Models;
using final.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace final.Services;

/// <summary>
/// BookService is meant to be a source of truth data source of Books
/// </summary>
public class BookService : IBookService
{

    // When I remove this, it won't matter. But since DbContext jazz is a scoped
    // subject, which means that it'll get handled for every request, the
    // BookService has to also be scoped. Which means that the BookService also
    // ends up losing its list between invocations. So, making bookCollection
    // static preserves the state for the runtime of the program. Normally,
    // shared-mutable state is a very bad thing to have, and will happily make
    // race-conditions happen. This is just for me to understand what's
    // happening underneath as I transition between using a list and using an
    // actual db context.
    private static List<Book> bookCollection = new();
    private BookContext? _context;

    // I must implement the following:
    // Create: Add, "Add a book to the collection"
    // Read: List, "List all books from the collection"
    // Read: Display, "Display information about a book by ID"
    // Update: Edit, "Change details about the book"
    // Delete: Remove, "Remove a book by ID"

    public BookService(BookContext? bookContext)
    {
        if (bookContext is not null)
        {
            _context = bookContext;
        }
    }

    /// <summary>
    /// AddBookRecord does what it says on the tin, checking for duplicates.
    /// </summary>
    /// <param name="book"></param>
    /// <returns>false if the book already exists</returns>
    public (bool success, int index) AddBookRecord(Book book)
    {
        (bool present, int index) = CheckForBook(book);
        if (present)
        {
            return (false, index);
        }
        int new_index = bookCollection.Count();
        bookCollection.Add(book);
        return (true, new_index);
    }

    /// <summary>
    /// GetAll does what it says on the tin.
    /// </summary>
    public List<Book> GetAll()
    {
        return bookCollection;
    }

    /// <summary>
    /// GetBookByIndex accepts a numeric ID and a callback exterior variable. It
    /// returns a bool, and the exterior value is modified if applicable.
    /// </summary>
    /// <param name="retrieval_index">index of the book to retrieve</param>
    /// <param name="book">externally modified Book</param>
    /// <returns>false if the book was not found</returns>
    public bool GetBookByIndex(int retrieval_index, out Book? book)
    {
        try
        {
            book = bookCollection[retrieval_index];
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
    /// <param name="book">Book whose existence is checked</param>
    /// <returns>bool condition of if the book input exists in the collection</returns>
    private (bool present, int index) CheckForBook(Book book)
    {
        int index = bookCollection.IndexOf(book);
        if (index < 0) return (false, index);
        return (true, index);
    }

    /// <summary>
    /// ReplaceBookRecord does what it says on the tin. The whole object at the
    /// given index is replaced.
    /// </summary>
    /// <param name="index">integer index to replace</param>
    /// <param name="book">replacement book data</param>
    /// <returns>(bool success, int index)The index will contain either the
    /// given index if successful, the count if out of range, or the index of
    /// the existing book if non-unique.</returns>
    public (bool success, int index) ReplaceBookRecord(int index, Book book)
    {
        try
        {
            // This method should reject a replacement book which is a duplicate
            // of another book.
            (bool present, int present_index) = CheckForBook(book);
            if (present) return (false, present_index);
            bookCollection[index] = book;
        }
        catch (ArgumentOutOfRangeException)
        {
            return (false, bookCollection.Count());
        }
        return (true, index);
    }

    /// <summary>
    /// RemoveSingleBookRecord takes a numeric ID and removes the corresponding
    /// record, if any.
    /// </summary>
    /// <param name="removal_index"></param>
    /// <returns>false if the book was not found</returns>
    public bool RemoveSingleBookRecord(int removal_index)
    {
        try
        {
            bookCollection.RemoveAt(removal_index);
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
        return true;
    }

}