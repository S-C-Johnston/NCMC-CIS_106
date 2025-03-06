namespace ex_QuestionsAndAnswers.models
{
    public class FormItem
    {
        public string Prompt { get; set; }
        public string? Response { get; set; }
        public bool IsOptional { get; set; }
    }
}
