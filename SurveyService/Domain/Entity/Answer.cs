namespace SurveyService.Domain.Entity;

public class Answer
{
    public long Id { get; set; }
    public long QuestionId { get; set; }
    public string Description { get; set; }
}