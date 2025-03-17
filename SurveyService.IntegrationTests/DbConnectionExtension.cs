using System.Data;
using Dapper;
using SurveyService.Domain.Entity;

namespace SurveyService.IntegrationTests;

public static class DbConnectionExtension
{
    public static async Task Truncate(this IDbConnection conn)
    {
        await conn.ExecuteAsync(@"
truncate Survey cascade;
truncate Interview cascade;
truncate Question cascade;
truncate Answer cascade;
truncate Result cascade;");
    }
    public static async Task<Survey> InsertSurvey(this IDbConnection conn, Survey survey)
    {
        return await conn.QuerySingleAsync<Survey>(@"
insert into Survey
    (Description)
values (@Description)
returning *", survey);
    }

    public static async Task<Interview> SelectInterview(this IDbConnection conn)
    {
        return await conn.QuerySingleAsync<Interview>(@"
select *
from Interview
");
    }
    
    public static async Task<Question> InsertQuestion(this IDbConnection conn, Question question)
    {
        return await conn.QuerySingleAsync<Question>(@"
insert into Question
    (SurveyId, Description, Index)
values (@SurveyId, @Description, @Index)
returning *", new{question.SurveyId, question.Description, question.Index});
    }

    public static async Task<Answer> InsertAnswer(this IDbConnection conn, Answer answer)
    {
        return await conn.QuerySingleAsync<Answer>(@"
insert into Answer
    (Description, QuestionId)
values (@Description, @QuestionId)
returning *", answer);
    }
}