namespace SurveyService.Domain.Entity;

public class Question
{
    public long Id;
    public long SurveyId;
    public string Text;
    public long? NextQuestionId;
}