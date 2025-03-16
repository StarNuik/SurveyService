namespace SurveyService.Dto;

public class QuestionResponse
{
    public string Text { get; set; }
    public QuestionResponseAnswer[] Answers { get; set; }
}

public class QuestionResponseAnswer
{
    public long Id { get; set; }
    public string Text { get; set; }
}