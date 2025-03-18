namespace SurveyService.Domain.Entity;

public class Survey
{
    public long Id { get; set; }
    public string Description { get; set; }
    public long[] QuestionIds { get; set; }
}