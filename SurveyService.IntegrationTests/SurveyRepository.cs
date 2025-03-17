using System.Data;
using Dapper;
using Npgsql;
using SurveyService.Domain.Entity;

namespace SurveyService.IntegrationTests;

public class SurveyRepository
{
    private const string connectionString = "Server=localhost;Port=5432;Userid=postgres;Password=postgres;Database=postgres";
    
    [Fact]
    public async void GetSurvey_Success()
    {
        // Arrange
        var survey = new Survey { FirstQuestionId = -1, };
        
        var conn = ConnectionFactory();
        await conn.ExecuteAsync(@"
insert into ""Survey""
    (""FirstQuestionId"")
values (@FirstQuestionId)", survey);
        
        var repo = new Infrastructure.SurveyRepository(ConnectionFactory);
        // Act
        // repo.GetSurvey()
        // Assert
    }

    private IDbConnection ConnectionFactory()
    {
        return new NpgsqlConnection(connectionString);
    }
}