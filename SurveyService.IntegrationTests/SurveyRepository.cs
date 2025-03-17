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
        using var conn = ConnectionFactory();
        await conn.Truncate();
        
        var survey = await conn.InsertSurvey("Survey Test");
        
        var repo = RepositoryFactory();
        
        // Act
        var response = await repo.GetSurvey(survey.Id);
        
        // Assert
        response
            .Should().BeEquivalentTo(survey);
    }

    [Fact]
    public async void InsertInterview_Success()
    {
        // Arrange
        using var conn = ConnectionFactory();
        await conn.Truncate();
        
        var survey = await conn.InsertSurvey("Survey Test");

        var repo = RepositoryFactory();

        // Act
        var request = new Interview
        {
            SurveyId = survey.Id,
            UserId = 123,
        };
        var response = await repo.InsertInterview(request);
        
        // Assert
        var interview = await conn.SelectInterview();
        response
            .Should().BeEquivalentTo(interview);
    }

    private Infrastructure.SurveyRepository RepositoryFactory()
    {
        return new Infrastructure.SurveyRepository(ConnectionFactory);
    }

    private IDbConnection ConnectionFactory()
    {
        return new NpgsqlConnection(connectionString);
    }
}