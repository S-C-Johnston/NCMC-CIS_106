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
    // -1 is the expected return value from IndexOf if the value doesn't
    // exist in the list, from which this is modified.
    const int NOT_FOUND_ID = -1;

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
        else
        {
            var factory = new BookContextFactory();
            _context = factory.CreateContext();
        }
    }

    /// <summary>
    /// AddBookRecord does what it says on the tin, checking for duplicates.
    /// </summary>
    /// <param name="book"></param>
    /// <returns>false if the book already exists</returns>
    public (bool success, int index) AddBookRecord(Book book)
    {
        (bool present, int present_id) = CheckForBook(book);
        if (present)
        {
            return (false, present_id);
        }
        //int new_index = bookCollection.Count();
        //bookCollection.Add(book);
        _context?.Books.Add(book);
        _context?.SaveChanges();
        return (true, book.Id);
    }

    /// <summary>
    /// GetAll does what it says on the tin.
    /// </summary>
    public List<Book> GetAll()
    {
        var bookList = _context?.Books.AsNoTracking().ToList() ?? bookCollection;
        if (bookCollection != bookList) {
            bookCollection = bookList;
        }
        return bookList;
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
    /// GetBookById accepts a numeric ID and a callback exterior variable. It
    /// returns a bool, and the exterior value is modified if applicable.
    /// </summary>
    /// <param name="retrieval_Id">Id of the book to retrieve</param>
    /// <param name="book">externally modified Book</param>
    /// <returns>false if the book was not found</returns>
    public bool GetBookById(int retrieval_Id, out Book? book)
    {
        book = _context?.Books.Find(retrieval_Id);
        if (book is null) return false;
        return true;
    }

    /// <summary>
    /// CheckForBook does what it says on the tin
    /// </summary>
    /// <param name="book">Book whose existence is checked</param>
    /// <returns>bool condition of if the book input exists in the collection</returns>
    private (bool present, int id) CheckForBook(Book book)
    {
        int id = Convert.ToInt32(
            _context?.Books
            .Where(w => w == book)
            .Select(s => s.Id)
            .FirstOrDefault());
            // UNSUPPORTED TRANSLATION OF DEFAULTS WITH PARAMS
            // I WASTED SO MUCH TIME HERE!!
            // RAGE
            // https://github.com/dotnet/efcore/issues/17783
        if (id <= 0) return (false, id);
        return (true, id);
    }

    /// <summary>
    /// ReplaceBookRecord does what it says on the tin. The whole object at the
    /// given Id is replaced.
    /// </summary>
    /// <param name="Id">integer Id to replace</param>
    /// <param name="book">replacement book data</param>
    /// <returns>(bool success, int Id)The Id will contain either the
    /// given Id if successful, the count if out of range, or the Id of
    /// the existing book if non-unique.</returns>
    public (bool success, int Id) ReplaceBookRecord(int Id, Book book)
    {
        try
        {
            // This method should reject a replacement book which is a duplicate
            // of another book.
            (bool present, int present_Id) = CheckForBook(book);
            if (present) return (false, present_Id);
            var target_book = _context?.Books.Find(Id);
            if (target_book is not null)
            {
                target_book = book;
                _context?.SaveChanges();
            }
            else
            {
                // I was relying on catching ArgumentNotInRangeException, and I
                // forgot to handle the case where a target book to update can't
                // be found.
                return (false, NOT_FOUND_ID);
            }
        }
        catch (DbUpdateException)
        {
            return (false, NOT_FOUND_ID);
        }
        return (true, Id);
    }

    /// <summary>
    /// RemoveSingleBookRecord takes a numeric ID and removes the corresponding
    /// record, if any.
    /// </summary>
    /// <param name="removal_Id"></param>
    /// <returns>false if the book was not found</returns>
    public bool RemoveSingleBookRecord(int removal_Id)
    {
        try
        {
            var removal_target = _context?.Books.Find(removal_Id);
            if (removal_target is not null) {
                _context?.Books.Remove(removal_target);
                _context?.SaveChanges();
            }
            else {
                // If the target can't be found, this is a 404
                return false;
            }
        }
        catch (DbUpdateException)
        {
            return false;
        }
        return true;
    }

}