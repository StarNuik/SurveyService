namespace SurveyService.Domain.Entity;

public class Interview
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long SurveyId { get; set; }
}