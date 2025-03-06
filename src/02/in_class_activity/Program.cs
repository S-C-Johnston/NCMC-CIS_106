using in_class_activity.models;

string favorite_color = "yellow";
string[] swallow_response = ["african", "european"];

List<FormItem> bridge_of_death_questions =
[
    new FormItem{
        Prompt = "What... is your name?",
        IsOptional = false
    },
    new FormItem{
        Prompt = "What... is your quest?",
        IsOptional = false
    },
    new FormItem{
        Prompt = "What... is your favorite color?",
        IsOptional = false
    },
    new FormItem{
        Prompt = "What... is the air-speed velocity of an unladen swallow?",
        IsOptional = true
    }
];

foreach (FormItem question in bridge_of_death_questions)
{
    do
    {
        Console.WriteLine(question.Prompt);
        Console.Write("\t?");
        string? my_response = Console.ReadLine();
        question.Response = my_response ?? "";

        if (question.Prompt.Contains("color") && !question.Response.ToLower().Contains(favorite_color))
        {
            Console.WriteLine("\nYou have questioned wrongly! [You are thrown off the bridge.]\n");
        }
        if (question.Prompt.Contains("swallow")
        && (
            question.Response.ToLower().Contains(swallow_response[0])
            || question.Response.ToLower().Contains(swallow_response[1])))
        {
            Console.WriteLine("\nI don't know! aaaugh [the sage is thrown off the bridge]\n");
        }
    }
    while (!question.IsOptional
    && String.IsNullOrWhiteSpace(question.Response));
}

Console.WriteLine(".\n.\n.");

foreach (FormItem answer in bridge_of_death_questions)
{
    Console.WriteLine("{0}\n\t{1}", answer.Prompt, answer.Response);
}
