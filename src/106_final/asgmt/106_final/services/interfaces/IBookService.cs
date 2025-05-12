using final.Models;

namespace final.Services.Interfaces;
public interface IBookService
{
    /// <summary>
    /// AddBookRecord does what it says on the tin, checking for duplicates.
    /// </summary>
    /// <param name="book"></param>
    /// <returns>false if the book already exists</returns>
    public (bool success, int index) AddBookRecord(Book book);

    /// <summary>
    /// GetAll does what it says on the tin.
    /// </summary>
    public List<Book> GetAll();

    /// <summary>
    /// GetBookByIndex accepts a numeric ID and a callback exterior variable. It
    /// returns a bool, and the exterior value is modified if applicable.
    /// </summary>
    /// <param name="retrieval_index">index of the book to retrieve</param>
    /// <param name="book">externally modified Book</param>
    /// <returns>false if the book was not found</returns>
    public bool GetBookByIndex(int retrieval_index, out Book? book);

    /// <summary>
    /// ReplaceBookRecord does what it says on the tin. The whole object at the
    /// given index is replaced.
    /// </summary>
    /// <param name="index">integer index to replace</param>
    /// <param name="book">replacement book data</param>
    /// <returns>(bool success, int index)The index will contain either the
    /// given index if successful, the count if out of range, or the index of
    /// the existing book if non-unique.</returns>
    public (bool success, int index) ReplaceBookRecord(int index, Book book);

    /// <summary>
    /// RemoveSingleBookRecord takes a numeric ID and removes the corresponding
    /// record, if any.
    /// </summary>
    /// <param name="removal_index"></param>
    /// <returns>false if the book was not found</returns>
    public bool RemoveSingleBookRecord(int removal_index);
}