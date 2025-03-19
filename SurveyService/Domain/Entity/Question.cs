namespace SurveyService.Domain.Entity;

public class Question
{
    public long Id { get; set; }
    public long SurveyId { get; set; }
    public string Description { get; set; }
}