using System.Data;
using Dapper;
using Npgsql;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Domain.Exception;

namespace SurveyService.Infrastructure;

public class SurveyRepository(IConfiguration configuration) : ISurveyRepository
{
    public Task<Survey> GetSurvey(long surveyId)
    {
        throw new NotImplementedException();
    }

    public Task<Interview> InsertInterview(Interview interview)
    {
        throw new NotImplementedException();
    }

    public async Task<Question> GetQuestion(long questionId)
    {
        try
        {
            using var conn = Connection();
            var question = await conn.QuerySingleAsync<Question>(
                "select * from \"Question\" where \"Id\" = @QuestionId",
                new { QuestionId = questionId }
            );
            return question;
        }
        catch (InvalidOperationException)
        {
            throw new NotFoundException();
        }
    }

    public async Task<Answer[]> GetAnswersOfQuestion(long questionId)
    {
        using var conn = Connection();
        var answers = await conn.QueryAsync<Answer>(
            "select * from answers where QuestionId = @QuestionId",
            new { QuestionId = questionId }
        );
        return answers.ToArray();
    }

    public Task InsertResult(Result result)
    {
        throw new NotImplementedException();
    }

    private IDbConnection Connection()
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        return new NpgsqlConnection(connectionString);
    }
}