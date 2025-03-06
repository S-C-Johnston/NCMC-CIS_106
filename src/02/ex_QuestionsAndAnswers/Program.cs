using ex_QuestionsAndAnswers.models;

FormItem formItem1 = new FormItem{
    Prompt = "What is your full name?",
    IsOptional = false
};

FormItem formItem2 = new FormItem{
    Prompt = "What city do you live in?",
    IsOptional = false
};

FormItem formItem3 = new FormItem{
    Prompt = "What is your favorite color?",
    IsOptional = true
};

FormItem formItem4 = new FormItem{
    Prompt = "What is your dog's name?",
    IsOptional = true
};

List<FormItem> formItems = new List<FormItem>();
formItems.Add(formItem1);
formItems.Add(formItem2);
formItems.Add(formItem3);
formItems.Add(formItem4);

foreach (FormItem item in formItems)
{
    do
    {
        Console.WriteLine(item.Prompt);

        string? userResponse = Console.ReadLine();

        if (!item.IsOptional && string.IsNullOrWhiteSpace(userResponse))
        {
            Console.WriteLine("Response required for this item. Please enter the correct information.");
        }
        else
        {
            item.Response = userResponse;
        }
    }
    while (!item.IsOptional && string.IsNullOrWhiteSpace(item.Response));
}

Console.WriteLine("FORM RESPONSES: ");
foreach (FormItem item in formItems)
{
    Console.WriteLine($"{item.Prompt}\n\t{item.Response}");
}
