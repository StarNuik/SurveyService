using System.Data;
using Dapper;
using Npgsql;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Domain.Exception;

namespace SurveyService.Infrastructure;

public class SurveyRepository(Func<IDbConnection> connectionFactory) : ISurveyRepository
{
    public async Task<Survey> GetSurvey(long surveyId)
    {
        using var conn = connectionFactory();

        var survey = await conn.QuerySingleAsync<Survey>(@"
select *
from Survey
where Id = @Id", new {Id = surveyId});

        return survey;
    }

    public async Task<Interview> InsertInterview(Interview request)
    {
        // using var conn = connectionFactory();
        //
        // var interview = await conn.QuerySingleAsync<Interview>(
        //     @"insert into ""Interview""
        //         values (@Interview)
        //         returning *"
        //     );
        throw new NotImplementedException();
    }

    public async Task<Question> GetQuestion(long questionId)
    {
        using var conn = connectionFactory();
        
        var question = await conn.QuerySingleAsync<Question>(
            "select * from \"Question\" where \"Id\" = @QuestionId",
            new { QuestionId = questionId }
        );
        
        return question;
    }

    public async Task<Answer[]> GetAnswersOfQuestion(long questionId)
    {
        using var conn = connectionFactory();
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

    // private IDbConnection Connection()
    // {
    //     var connectionString = configuration.GetConnectionString("Postgres");
    //     return new NpgsqlConnection(connectionString);
    // }
}