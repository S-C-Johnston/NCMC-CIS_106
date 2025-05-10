namespace final.Models;

/// <summary>
/// Book models a book, per the spec.
/// </summary>
/// <remarks>
/// Book is not opinionated about how it is managed, it is largely a collection
/// of state. If it were smaller, and of fixed size, it'd be a candidate for a
/// struct.
/// </remarks>
public class Book
{
public required string Title { get; set; }
public required string Author { get; set; }
public required string Genre { get; set; }
public required int ID { get; set; }
}
