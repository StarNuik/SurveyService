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
        
        var survey = await conn.InsertSurvey(new() {Description = "Test Survey"});
        
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
        
        var survey = await conn.InsertSurvey(new() {Description = "Test Survey"});

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

    [Fact]
    public async void GetQuestion_Success()
    {
        // Arrange
        using var conn = ConnectionFactory();
        await conn.Truncate();
        
        var survey = await conn.InsertSurvey(new() {Description = "Test Survey"});
        var question = await conn.InsertQuestion(new()
        {
            Description = "Test Question", Index = 25, SurveyId = survey.Id,
        });

        var repo = RepositoryFactory();
        
        // Act
        var response = await repo.GetQuestion(survey.Id, question.Index);
        
        // Assert
        response
            .Should().BeEquivalentTo(question);
    }

    [Fact]
    public async void GetAnswersOfQuestion_Success()
    {
        // Arrange
        using var conn = ConnectionFactory();
        await conn.Truncate();
        
        var survey = await conn.InsertSurvey(new() {Description = "Test Survey"});
        var question = await conn.InsertQuestion(new()
        {
            Description = "Test Question", Index = 25, SurveyId = survey.Id,
        });

        var answers = new Answer[3];
        answers[0] = await conn.InsertAnswer(new() { Description = "Answer 0", QuestionId = question.Id });
        answers[1] = await conn.InsertAnswer(new() { Description = "Answer 1", QuestionId = question.Id });
        answers[2] = await conn.InsertAnswer(new() { Description = "Answer 2", QuestionId = question.Id });

        var repo = RepositoryFactory();
        
        // Act
        var response = await repo.GetAnswersOfQuestion(question.Id);

        // Assert
        response
            .Should().BeEquivalentTo(answers);
    }

    [Fact]
    public async void InsertResult_Success()
    {
        // Arrange
        using var conn = ConnectionFactory();
        await conn.Truncate();

        var userId = 123;
        var survey = await conn.InsertSurvey(new() {Description = "Test Survey"});
        var question = await conn.InsertQuestion(new()
        {
            Description = "Test Question", Index = 25, SurveyId = survey.Id,
        });

        var answers = new Answer[3];
        answers[0] = await conn.InsertAnswer(new() { Description = "Answer 0", QuestionId = question.Id });
        answers[1] = await conn.InsertAnswer(new() { Description = "Answer 1", QuestionId = question.Id });
        answers[2] = await conn.InsertAnswer(new() { Description = "Answer 2", QuestionId = question.Id });

        var interview = await conn.InsertInterview(new() { UserId = userId, SurveyId = survey.Id });

        var repo = RepositoryFactory();
        
        // Act
        var request = new Result
        {
            InterviewId = interview.Id,
            AnswerId = answers[1].Id,
        };
        await repo.InsertResult(request);
        
        // Assert
        var result = await conn.SelectResult();
        result.AnswerId
            .Should().Be(request.AnswerId);
        result.InterviewId
            .Should().Be(request.InterviewId);
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