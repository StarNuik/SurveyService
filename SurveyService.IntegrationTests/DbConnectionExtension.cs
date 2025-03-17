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
    public static async Task<Survey> InsertSurvey(this IDbConnection conn, string description)
    {
        return await conn.QuerySingleAsync<Survey>(@"
insert into Survey
    (Description)
values (@Description)
returning *", new { Description = description, });
    }

//     public static async Task<Interview> InsertInterview(this IDbConnection conn, long userId, long surveyId)
//     {
//         return await conn.QuerySingleAsync<Interview>(@"
// insert into Interview
//     (UserId, SurveyId)
// values (@UserId, @SurveyId)
// returning *", new { UserId = userId, SurveyId = surveyId, });
//     }

    public static async Task<Interview> SelectInterview(this IDbConnection conn)
    {
        return await conn.QuerySingleAsync<Interview>(@"
select *
from Interview
");
    }
}