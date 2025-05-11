using final.Models;

namespace final.Services;

/// <summary>
/// BookManagerService is meant to be instantiated and run with .Dispatch().
/// It's rather a program in and of itself. The object tracks state internally,
/// and does not export it in any way.
/// </summary>
public class BookManagerService
{

    private Dictionary<int, Book> bookCollection = new();

    // I must implement the following:
    // Create: Add, "Add a book to the collection"
    // Read: List, "List all books from the collection"
    // Read: Display, "Display information about a book by ID"
    // Update: Edit, "Change details about the book"
    // Delete: Remove, "Remove a book by ID"
}