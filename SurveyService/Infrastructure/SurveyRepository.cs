using System.Data;
using Dapper;
using SurveyService.Domain;
using SurveyService.Domain.Entity;

namespace SurveyService.Infrastructure;

public class SurveyRepository(Func<IDbConnection> connectionFactory) : ISurveyRepository
{
    public async Task<Survey> GetSurvey(long surveyId)
    {
        using var conn = connectionFactory();

        var survey = await conn.QuerySingleAsync<Survey>(
            """
            select *
            from Survey
            where Id = @Id
            """, new { Id = surveyId });

        return survey;
    }

    public async Task<Survey[]> GetAllSurveys()
    {
        using var conn = connectionFactory();

        var surveys = await conn.QueryAsync<Survey>(
            """
            select *
            from Survey
            """);

        return surveys.ToArray();
    }

    public async Task<Interview> InsertInterview(Interview request)
    {
        using var conn = connectionFactory();

        var interview = await conn.QuerySingleAsync<Interview>(
            """
            insert into Interview
                (UserId, SurveyId)
            values 
                (@UserId, @SurveyId)
            returning *
            """, request);

        return interview;
    }

    public async Task<Question> GetQuestion(long questionId)
    {
        using var conn = connectionFactory();

        var question = await conn.QuerySingleAsync<Question>(
            """
            select *
            from Question
            where Id=@QuestionId
            """, new { QuestionId = questionId });

        return question;
    }

    public async Task<Answer[]> GetAnswersOfQuestion(long questionId)
    {
        using var conn = connectionFactory();

        var answers = await conn.QueryAsync<Answer>(
            """
            select *
            from Answer
            where QuestionId = @QuestionId
            """, new { QuestionId = questionId });

        return answers.ToArray();
    }

    public async Task InsertResult(Result result)
    {
        using var conn = connectionFactory();

        await conn.ExecuteAsync(
            """
            insert into Result
                (InterviewId, AnswerId)
            values 
                (@InterviewId, @AnswerId)
            """, result);
    }
}