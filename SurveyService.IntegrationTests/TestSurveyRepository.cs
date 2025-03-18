using System.Data;
using Dapper;
using Npgsql;
using SurveyService.Domain.Entity;

namespace SurveyService.IntegrationTests;

public class TestSurveyRepository : IDisposable
{
    private const string connectionString =
        "Server=localhost;Port=5432;Userid=postgres;Password=postgres;Database=postgres";

    private readonly IDbConnection conn;

    public TestSurveyRepository()
    {
        conn = ConnectionFactory();
    }
    
    public void Dispose()
    {
        conn.Dispose();
    }

    public Infrastructure.SurveyRepository NewSurveyRepository()
    {
        return new Infrastructure.SurveyRepository(ConnectionFactory);
    }

    public async Task Truncate()
    {
        await conn.ExecuteAsync(
            """
            truncate Survey cascade;
            truncate Interview cascade;
            truncate Question cascade;
            truncate Answer cascade;
            truncate Result cascade;
            """);
    }

    public async Task<Survey> InsertSurvey(Survey survey)
    {
        return await conn.QuerySingleAsync<Survey>(
            """
            insert into Survey
                (Description)
            values (@Description)
            returning *
            """, survey);
    }

    public async Task<Interview> SelectInterview()
    {
        return await conn.QuerySingleAsync<Interview>(
            """
            select *
            from Interview
            """);
    }

    public async Task<Question> InsertQuestion(Question question)
    {
        return await conn.QuerySingleAsync<Question>(
            """
            insert into Question
                (SurveyId, Description, Index)
            values (@SurveyId, @Description, @Index)
            returning *
            """,
            new
            {
                question.SurveyId,
                question.Description,
                question.Index
            });
    }

    public async Task<Answer> InsertAnswer(Answer answer)
    {
        return await conn.QuerySingleAsync<Answer>(
            """
            insert into Answer
                (Description, QuestionId)
            values (@Description, @QuestionId)
            returning *
            """, answer);
    }

    public async Task<Interview> InsertInterview(Interview interview)
    {
        return await conn.QuerySingleAsync<Interview>(
            """
            insert into Interview
                (UserId, SurveyId)
            values (@UserId, @SurveyId)
            returning *
            """, interview);
    }

    public async Task<Result> SelectResult()
    {
        return await conn.QuerySingleAsync<Result>(
            """
            select *
            from Result
            """);
    }

    private static IDbConnection ConnectionFactory()
    {
        return new NpgsqlConnection(connectionString);
    }
}