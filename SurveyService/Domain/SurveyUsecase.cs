using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.Domain;

public class SurveyUsecase(ISurveyRepository repo)
{
    public async Task<PostInterviewResponse> NewInterview(PostInterviewRequest request)
    {
        // throws if the survey doesn't exist
        var survey = await repo.GetSurvey(request.SurveyId);

        var interviewRequest = new Interview
        {
            SurveyId = survey.Id,
            UserId = request.UserId,
        };
        var interview = await repo.InsertInterview(interviewRequest);

        var questionResponse = await GetQuestion(survey.FirstQuestionId);

        var dto = new PostInterviewResponse
        {
            FirstQuestion = questionResponse,
            InterviewId = interview.Id,
        };
        return dto;
    }
    
    public async Task<GetQuestionResponse> GetQuestion(long questionId)
    {
        var question = await repo.GetQuestion(questionId);

        var answers = await repo.GetAnswersOfQuestion(questionId);

        var dto = MakeQuestionResponse(question, answers);

        return dto;
    }

    public async Task SaveResult(PostResultRequest request)
    {
        var result = new Result
        {
            AnswerId = request.AnswerId,
            InterviewId = request.InterviewId,
        };
        await repo.InsertResult(result);
    }

    private GetQuestionResponse MakeQuestionResponse(Question question, Answer[] answers)
    {
        return new GetQuestionResponse
        {
            Text = question.Text,
            HasNextQuestion = question.NextQuestionId.HasValue,
            NextQuestionId = question.NextQuestionId.GetValueOrDefault(),
            Answers = answers
                .Select(
                    from => new GetQuestionResponseAnswer
                    {
                        Id = from.Id,
                        Text = from.Text
                    }
                ).ToArray()
        };
    }
}