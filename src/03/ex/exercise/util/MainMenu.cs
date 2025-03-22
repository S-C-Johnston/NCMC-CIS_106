namespace exercise.util;

/// <summary>
/// MainMenuOptions is what it says on the tin, but doing it with enums so we're
/// not checking numbers manually.
/// </summary>
public enum MainMenuOptions {
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Exit
}

/// <summary>
/// MainMenu is the util class for Main Menu operations
/// </summary>
public class MainMenu
{
    private void PrintMainMenu()
    {
        foreach (MainMenuOptions menu_option in Enum.GetValues<MainMenuOptions>())
        {
            Console.Write("{0}: {1}\n",
            menu_option.GetHashCode(),
            menu_option);
        }
    }

    /// <summary>
    /// PromptMainMenu prompts the user at the console for responses based on an
    /// enum-driven menu.
    /// </summary>
    /// <remarks>
    /// Thankfully, Enum.TryParse is strong enough that it can interpret exact
    /// string matches *or* appropriate underlying type. However, it doesn't
    /// introspect to if a given numeric value matching the underlying type is
    /// actually valid, so we use IsDefined to check. E.g, with 5 elements to
    /// the enum, the index "5" would be one out of bounds. TryParse accepts it,
    /// but IsDefined does not. That said, we need to use return values from
    /// both TryParse && IsDefined, because TryParse will assign the default
    /// value to the out result even when it's false.
    /// </remarks>
    /// <returns>The enumerated value input by the user.</returns>
    public MainMenuOptions PromptMainMenu()
    {
        bool previously_run = false;
        MainMenuOptions result;
        string maybe_menu_option;
        do
        {
            if (previously_run) Console.WriteLine("Main menu input must be valid!");
            PrintMainMenu();
            previously_run = true;
            Console.Write("Option (number or exact string)?: ");
            string? user_input = Console.ReadLine();
            maybe_menu_option = user_input ?? "";
        } while (!
        (MainMenuOptions.TryParse(maybe_menu_option, out result)
        && Enum.IsDefined<MainMenuOptions>(result)));
        return result;
    }
}
