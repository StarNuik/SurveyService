using System.Data;
using Dapper;
using FluentAssertions;
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
        var survey = new Survey { Description = "Survey Test", };
        
        using var conn = ConnectionFactory();
        survey = await conn.QuerySingleAsync<Survey>(@"
insert into Survey
    (Description)
values (@Description)
returning *", survey);
        
        var repo = new Infrastructure.SurveyRepository(ConnectionFactory);
        
        // Act
        var response = await repo.GetSurvey(survey.Id);
        
        // Assert
        response
            .Should().BeEquivalentTo(survey);
    }

    private IDbConnection ConnectionFactory()
    {
        return new NpgsqlConnection(connectionString);
    }
}