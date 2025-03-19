using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.IntegrationTests;

[Collection("uses_postgres")]
public class SurveyController_PostResult(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly SurveyClient client = new(factory.CreateClient());

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public async Task IncorrectIds_BadRequest(bool incorrectInterviewId, bool incorrectAnswerId)
    {
        // Arrange
        using var repo = new TestSurveyRepository();
        await repo.Truncate();
        
        var userId = 123; 
        var survey = await repo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = [],
        });
        var interview = await repo.InsertInterview(new Interview
        {
            SurveyId = survey.Id,
            UserId = userId
        });
        var question = await repo.InsertQuestion(new Question
        {
            Description = "Test Question",
            SurveyId = survey.Id,
        });
        var answer = await repo.InsertAnswer(new Answer
        {
            Description = "Test Answer",
            QuestionId = question.Id
        });
        
        var request = new PostResultRequest
        {
            AnswerId = incorrectAnswerId ? -1 : answer.Id,
            InterviewId = incorrectInterviewId? -1 : interview.Id
        };
        
        // Act
        var call = async () => await client.PostResult(request);
        
        // Assert
        await call
            .Should().ThrowAsync<HttpRequestException>()
            .WithMessage("*400*");
    }
    
    [Fact]
    public async Task HappyPath_Success()
    {
        // Arrange
        using var repo = new TestSurveyRepository();
        await repo.Truncate();

        var userId = 123; 
        var survey = await repo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = [],
        });
        var interview = await repo.InsertInterview(new Interview
        {
            SurveyId = survey.Id,
            UserId = userId
        });
        var question = await repo.InsertQuestion(new Question
        {
            Description = "Test Question",
            SurveyId = survey.Id,
        });
        var answer = await repo.InsertAnswer(new Answer
        {
            Description = "Test Answer",
            QuestionId = question.Id
        });
        
        // Act
        var request = new PostResultRequest
        {
            AnswerId = answer.Id,
            InterviewId = interview.Id
        };
        await client.PostResult(request);

        // Assert
        var result = await repo.SelectResult();
        result.InterviewId
            .Should().Be(interview.Id);
        result.AnswerId
            .Should().Be(answer.Id);
    }
}