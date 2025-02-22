using System.Reflection;

namespace assignment.models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateOnly PublishingDate { get; set; }

        public Book(
            string title,
            string author,
            DateOnly publishingDate
            )
        {
            Title = title;
            Author = author;
            PublishingDate = publishingDate;
        }
    }
}