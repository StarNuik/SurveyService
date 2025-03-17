using SurveyService.Domain.Entity;

namespace SurveyService.Domain;

public interface ISurveyRepository
{
    public Task<Survey> GetSurvey(long surveyId);
    public Task<Interview> InsertInterview(Interview interview);
    public Task<Question> GetQuestion(long surveyId, long questionIndex);
    public Task<Answer[]> GetAnswersOfQuestion(long questionId);
    public Task InsertResult(Result result);
}