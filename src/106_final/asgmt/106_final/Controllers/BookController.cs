using final.Models;
using final.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    public BookController()
    {
    }

    // GET all
    /// <summary>
    /// GetAll does what it says on the tin, but returns the results as a sorted
    /// list with the index as the key
    /// </summary>
    /// <returns>SortedList<int,Book> where the index is the key</returns>
    [HttpGet]
    public ActionResult<SortedList<int,Book>> GetAll()
    {
        return new SortedList<int, Book>(
         BookService.GetAll()
         .Select((book, index) => new { index, book })
         .ToDictionary(
             selection => selection.index,
             selection => selection.book));
    }

    // GET individual
    /// <summary>
    /// Get takes an integer id, which is the index of the item in the list.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Book record</returns>
    [HttpGet("{id:int}")]
    public ActionResult<Book> Get(int id)
    {
        Book? book;
        BookService.GetBookByIndex(id, out book);
        if(null == book) return NotFound();
        return book;
    }

    // POST
    /// <summary>
    /// Create does what it says on the tin.
    /// </summary>
    /// <param name="book">[ApiContoller] at this route means that the Book is
    /// expected in the request body.</param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create(Book book)
    {
        (bool success, int index) = BookService.AddBookRecord(book);
        if (!success) return Conflict();
        // It took some time to understand CreatedAtAction; you can't pass just
        // the raw value as such, because the new anonymous object created here
        // is the *whole* object passed to the action, which is expecting named
        // parameters. So the reference type I was trying to use, of just the
        // bare index, was not in the correct format.
        return CreatedAtAction(nameof(Get), new { id = index }, book);
    }

    // PUT replace individual
    /// <summary>
    /// Update does what it says on the tin. Whole object replacement, not
    /// patching.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="book">[ApiController] on this route means that Book is
    /// expected in the request body</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Book book)
    {
        bool success = BookService.ReplaceBookRecord(id, book);
        return success ? NoContent() : NotFound();
    }

    // DELETE individual
    /// <summary>
    /// Delete does what it says on the tin.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        bool success = BookService.RemoveSingleBookRecord(id);
        return success ? NoContent() : NotFound();
    }
}