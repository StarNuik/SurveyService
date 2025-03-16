using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.Domain;

public class SurveyUsecase(ISurveyRepository repo)
{
    public async Task<QuestionResponse> GetQuestion(long questionId)
    {
        var question = await repo.GetQuestion(questionId);

        var answers = await repo.GetAnswersOfQuestion(questionId);

        var dto = MakeQuestionResponse(question, answers);

        return dto;
    }
    
    public Task SaveAnswer(long userId, long answerId)
    {
        // insert UserId, AnswerId
        throw new NotImplementedException();
    }

    private QuestionResponse MakeQuestionResponse(Question question, Answer[] answers)
    {
        return new QuestionResponse
        {
            Text = question.Text,
            Answers = answers
                .Select(
                    from => new QuestionResponseAnswer
                    {
                        Id = from.Id,
                        Text = from.Text
                    }
                ).ToArray()
        };
    }
}