namespace assignment.models
{
    public class Bookshelf
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public List<Book> Books { get; set; } = new List<Book> {};
    }
}