using exercise.util;

NumberPrompt np = new();
MainMenu mm = new();

bool exit = false;
do
{

    MainMenuOptions mainOperation = mm.PromptMainMenu();
    if (MainMenuOptions.Exit == mainOperation)
    {
        Console.WriteLine("Goodbye!");
        exit = true;
        continue;
    }

    double leftHandOperand = np.PromptForNumber();
    double rightHandOperand = np.PromptForNumber();

    switch (mainOperation)
    {
        case MainMenuOptions.Addition:
            Console.WriteLine("{0} + {1} = {2}",
            leftHandOperand,
            rightHandOperand,
            (leftHandOperand + rightHandOperand)
            );
            break;
        case MainMenuOptions.Subtraction:
            Console.WriteLine("{0} - {1} = {2}",
            leftHandOperand,
            rightHandOperand,
            (leftHandOperand - rightHandOperand)
            );
            break;
        case MainMenuOptions.Division:
            Console.WriteLine("{0} / {1} = {2}",
            leftHandOperand,
            rightHandOperand,
            (leftHandOperand / rightHandOperand)
            );
            break;
        case MainMenuOptions.Multiplication:
            Console.WriteLine("{0} * {1} = {2}",
            leftHandOperand,
            rightHandOperand,
            (leftHandOperand * rightHandOperand)
            );
            break;
        default:
            break;
    }

} while (!exit);
