using SurveyService.Domain.Entity;

namespace SurveyService.Domain;

public interface ISurveyRepository
{
    public Task<Question> GetQuestion(long questionId);
    public Task<Answer[]> GetAnswersOfQuestion(long questionId);
}