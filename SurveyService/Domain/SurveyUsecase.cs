using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.Domain;

public class SurveyUsecase(ISurveyRepository repo)
{
    public async Task<GetAllSurveysResponse> GetAllSurveys()
    {
        var surveys = await repo.GetAllSurveys();

        var dto = Mapper.MakeAllSurveysResponse(surveys);

        return dto;
    }
    
    public async Task<PostInterviewResponse> NewInterview(PostInterviewRequest request)
    {
        var survey = await repo.GetSurvey(request.SurveyId);

        var interview = await repo.InsertInterview(new Interview
        {
            SurveyId = survey.Id,
            UserId = request.UserId
        });

        var dto = new PostInterviewResponse
        {
            InterviewId = interview.Id,
            QuestionIds = survey.QuestionIds
        };
        return dto;
    }

    public async Task<GetQuestionResponse> GetQuestion(long questionId)
    {
        var question = await repo.GetQuestion(questionId);

        var answers = await repo.GetAnswersOfQuestion(questionId);

        var dto = Mapper.MakeQuestionResponse(question, answers);

        return dto;
    }

    public async Task SaveResult(PostResultRequest request)
    {
        var result = new Result
        {
            AnswerId = request.AnswerId,
            InterviewId = request.InterviewId
        };
        await repo.InsertResult(result);
    }
}