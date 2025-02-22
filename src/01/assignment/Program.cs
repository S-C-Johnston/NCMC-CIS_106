using System.Collections.Immutable;
using assignment.models;
using Microsoft.VisualBasic;

Bookshelf my_bookshelf = new Bookshelf();

Book hitchhikers_guide = new Book (
    "Hitchhiker's Guide to the Galaxy",
    "Douglas Adams",
    DateOnly.Parse("1979-10-12")
);

Book restuarant = new Book (
    "The Restuarant at the End of the Universe",
    "Douglas Adams",
    DateOnly.Parse("1980-10-01")
);

Console.WriteLine($"The bookshelf should be empty...");
Console.WriteLine($"Bookshelf {my_bookshelf.Guid} has books {String.Join(',', my_bookshelf.Books.Select(b=>b.Title).ToList())}");

my_bookshelf.Books.Add(hitchhikers_guide);
my_bookshelf.Books.Add(restuarant);

Console.WriteLine($"Added books...");
Console.WriteLine($"Bookshelf {my_bookshelf.Guid} has books {String.Join(',', my_bookshelf.Books.Select(b=>b.Title).ToList())}");