namespace final.Models;

/// <summary>
/// Book models a book, per the spec.
/// </summary>
/// <remarks>
/// Book is not opinionated about how it is managed, it is largely a collection
/// of state. If it were smaller, and of fixed size, it'd be a candidate for a
/// struct.
/// </remarks>
public class Book : IEquatable<Book>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string Genre { get; set; }

    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        else
        {
            return this.Equals((Book)obj);
        }
    }
    public bool Equals(Book? book)
    {
        if (book is null) return false;
        return this.Author == book.Author &&
        this.Title == book.Title &&
        this.Genre == book.Genre;
    }

    public override int GetHashCode() => (Title, Author, Genre).GetHashCode();

    public static bool operator ==(Book? lhs, Book? rhs)
    {
        if (lhs is null) {
            if (rhs is null)
            {
                return true;
            }
            return false;
        }
        return lhs.Equals(rhs);
    }
    public static bool operator !=(Book lhs, Book rhs) => !(lhs == rhs);

}
