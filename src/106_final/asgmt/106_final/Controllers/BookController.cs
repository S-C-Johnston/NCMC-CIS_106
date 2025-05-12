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
    // PUT replace individual
    // DELETE individual
}