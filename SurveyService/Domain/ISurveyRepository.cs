using SurveyService.Domain.Entity;

namespace SurveyService.Domain;

public interface ISurveyRepository
{
    public Task<Survey> GetSurvey(long surveyId);
    public Task<Survey[]> GetAllSurveys();
    public Task<Interview> InsertInterview(Interview interview);
    public Task<Question> GetQuestion(long questionId);
    public Task<Answer[]> GetAnswersOfQuestion(long questionId);
    public Task InsertResult(Result result);
}