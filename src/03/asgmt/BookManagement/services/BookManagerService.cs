using System.Reflection;
using BookManagement.Models;

namespace BookManagement.Services;

public enum BookManagementMenuItems
{
    None,
    Add,
    List,
    Display,
    Remove,
    Help,
    Exit
}

public class BookManagerService
{

    private Dictionary<int, Book> bookCollection = new();

    public void Dispatch()
    {
        bool exit = false;
        do
        {
            BookManagementMenuItems current_choice = PromptForMenu();
            if (BookManagementMenuItems.Exit == current_choice) break;

            switch (current_choice)
            {
                default:
                    Console.WriteLine("Not yet implemented!");
                    break;
            }
        } while (!exit);
    }

    private Dictionary<BookManagementMenuItems, string> MenuItemExplanatory = new() {
            { BookManagementMenuItems.Add, "Add a book to the collection" },
            { BookManagementMenuItems.List, "List all books from the collection" },
            { BookManagementMenuItems.Display, "Display information about a book by ID" },
            { BookManagementMenuItems.Remove, "Remove a book by ID" },
            { BookManagementMenuItems.Help, "Print this menu" },
            { BookManagementMenuItems.Exit, "Exit the program" }
    };
    private void PrintMenu()
    {
        foreach (var item_explanation in MenuItemExplanatory)
        {
            Console.WriteLine("{0} {1,-8}: {2}",
            item_explanation.Key.GetHashCode(),
            item_explanation.Key,
            item_explanation.Value
            );
        }
    }

    private BookManagementMenuItems PromptForMenu()
    {
        bool previously_run = false;
        BookManagementMenuItems result;
        string maybe_menu_item;
        do
        {
            if (previously_run) Console.WriteLine("Try again with a valid choice");
            PrintMenu();
            Console.Write("Number for menu option?: ");
            string? user_input = Console.ReadLine();
            maybe_menu_item = user_input ?? "";
        } while (!
        (Enum.TryParse<BookManagementMenuItems>(maybe_menu_item, out result)
        && Enum.IsDefined<BookManagementMenuItems>(result)));
        return result;
    }
}