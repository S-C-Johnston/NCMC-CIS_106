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
    /// given Id is replaced.
    /// </summary>
    /// <param name="Id">integer Id to replace</param>
    /// <param name="book">replacement book data</param>
    /// <returns>(bool success, int Id)The Id will contain either the
    /// given Id if successful, the count if out of range, or the Id of
    /// the existing book if non-unique.</returns>
    public (bool success, int Id) ReplaceBookRecord(int Id, Book book);

    /// <summary>
    /// RemoveSingleBookRecord takes a numeric ID and removes the corresponding
    /// record, if any.
    /// </summary>
    /// <param name="removal_Id"></param>
    /// <returns>false if the book was not found</returns>
    public bool RemoveSingleBookRecord(int removal_Id);

    /// <summary>
    /// GetBookById accepts a numeric ID and a callback exterior variable. It
    /// returns a bool, and the exterior value is modified if applicable.
    /// </summary>
    /// <param name="retrieval_Id">Id of the book to retrieve</param>
    /// <param name="book">externally modified Book</param>
    /// <returns>false if the book was not found</returns>
    public bool GetBookById(int retrieval_Id, out Book? book);
}