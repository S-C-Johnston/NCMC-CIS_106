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

    private Dictionary<BookManagementMenuItems, string> MenuItemExplanatory = new() {
            { BookManagementMenuItems.Add, "Add a book to the collection" },
            { BookManagementMenuItems.List, "List all books from the collection" },
            { BookManagementMenuItems.Display, "Display information about a book by ID" },
            { BookManagementMenuItems.Remove, "Remove a book by ID" },
            { BookManagementMenuItems.Help, "Print this menu" },
            { BookManagementMenuItems.Exit, "Exit the program" }
    };

    public void Dispatch()
    {
        bool exit = false;
        do
        {
            BookManagementMenuItems current_choice = PromptForMenu();
            if (BookManagementMenuItems.Exit == current_choice) break;

            switch (current_choice)
            {
                case BookManagementMenuItems.Add:
                    Book new_book = PromptForBookDetails();
                    bookCollection.TryAdd(new_book.ID, new_book);
                    break;
                default:
                    Console.WriteLine("Not yet implemented!");
                    break;
            }
        } while (!exit);
    }

    /// <summary>
    /// PromptForBookDetails writes prompts in a loop. It is deliberatly not
    /// opinionated except with respect to the uniqueness of an ID, and then
    /// only because I don't want to litter side effects in the function. It
    /// depends on the state of the object, however.
    /// </summary>
    /// <returns>Book object filled with those details</returns>
    private Book PromptForBookDetails()
    {
        bool previously_run = false;
        bool valid_int = false;
        bool valid_ID = false;
        int input_ID = default;
        bool user_satisfied = false;
        Dictionary<string, string> user_inputs = new(){
            {"Title", ""},
            {"Author", ""},
            {"Genre", ""} };

        do
        {
            if (previously_run)
            {
                if (!valid_int) Console.WriteLine("ID must be valid integer!");
                if (!valid_ID) Console.WriteLine("ID must not already exist!");
            }

            string existing_ID = default != input_ID ? $" [{input_ID}]" : "";

            Console.Write("\nEnter the book's integer ID{0}: ", existing_ID);
            string user_input = Console.ReadLine() ?? "";
            int this_loop_input_ID = default;
            valid_int = int.TryParse(user_input, out this_loop_input_ID);
            if ((default != this_loop_input_ID)
            && (this_loop_input_ID != input_ID))
            {
                input_ID = this_loop_input_ID;
            }
            valid_ID = !bookCollection.Keys.Contains(input_ID);
            if (string.IsNullOrWhiteSpace(user_input) && previously_run)
            {
                valid_int = valid_ID;
            }

            previously_run = true;
            if (!valid_int || !valid_ID) continue;

            foreach (KeyValuePair<string, string> input_item in user_inputs)
            {
                string existing_value
                = !string.IsNullOrWhiteSpace(input_item.Value)
                ? $" [{input_item.Value}]"
                : "";

                Console.Write("\nEnter the book {0}{1}: ",
                input_item.Key,
                existing_value);

                user_input = Console.ReadLine() ?? "";
                user_inputs[input_item.Key] = !string.IsNullOrWhiteSpace(user_input)
                ? user_input
                : input_item.Value;
                // I have decided not to be opinionated about any minimum length
                // for a Title, Author, or Genre. I print out what they've
                // written so far for their satisfaction so they can check it.
                // It's their data.
            }

            Console.WriteLine("\nYou entered...");
            Console.WriteLine("{0,10} : {1}", "ID", input_ID);
            foreach (KeyValuePair<string, string> input_item in user_inputs)
            {
                Console.WriteLine($"{input_item.Key,10} : {input_item.Value}");
            }
            Console.Write("Is all this correct? [y]/n: ");
            string satisfaction_prompt = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(satisfaction_prompt))
            {
                user_satisfied = string.Equals($"{satisfaction_prompt.ToLower().First()}", "y");
            }
            else
            {
                user_satisfied = true;
            }
        } while (!user_satisfied);

        return new Book{
            Title = user_inputs["Title"],
            Author = user_inputs["Author"],
            Genre = user_inputs["Genre"],
            ID = input_ID
        };
    }

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
            previously_run = true;
        } while (!
        (Enum.TryParse<BookManagementMenuItems>(maybe_menu_item, out result)
        && Enum.IsDefined<BookManagementMenuItems>(result)));
        return result;
    }
}