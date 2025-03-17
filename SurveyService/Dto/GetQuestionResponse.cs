namespace SurveyService.Dto;

public class GetQuestionResponse
{
    public string Text;
    public GetQuestionResponseAnswer[] Answers;
    public bool HasNextQuestion;
    public long NextQuestionId;
}

public class GetQuestionResponseAnswer
{
    public long Id;
    public string Text;
}