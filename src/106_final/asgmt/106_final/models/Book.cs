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
// TODO: Evaluate the utility of this? I think I was thinking, at the time I
// wrote the original code, that this would be easier to return as a whole
// object. I don't remember why I thought that. Maybe my thinking was like "ISBN
// is a thing, so each book should have an ID".
public required int ID { get; set; }
}
