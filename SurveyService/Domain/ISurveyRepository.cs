namespace SurveyService.Domain;

public interface ISurveyRepository
{
    public Task<Entity.Question> GetQuestion(long questionId);
    public Task<Entity.Answer[]> GetAnswersOfQuestion(long questionId);
}