using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.IntegrationTests;

[Collection("uses_postgres")]
public class SurveyController_NewInterview(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly SurveyClient client = new(factory.CreateClient());

    [Fact]
    public async Task IncorrectSurveyId_BadRequest()
    {
        // Arrange
        var repo = new TestSurveyRepository();
        await repo.Truncate();

        var userId = 123;
        
        // Act
        var request = new PostInterviewRequest
        {
            SurveyId = -1,
            UserId = userId,
        };
        var call = async () => await client.PostNewInterview(request);;

        // Assert
        await call
            .Should().ThrowAsync<Exception>()
            .WithMessage("*400*");
    }

    [Fact]
    public async Task HappyPath_Success()
    {
        // Arrange
        var repo = new TestSurveyRepository();
        await repo.Truncate();

        var userId = 123; 
        var survey = await repo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = [1, 2, 3]
        });
        
        // Act
        var request = new PostInterviewRequest
        {
            SurveyId = survey.Id,
            UserId = userId,
        };
        var response = await client.PostNewInterview(request);

        // Assert
        var interview = await repo.SelectInterview();

        response.InterviewId
            .Should().Be(interview.Id);

        response.QuestionIds
            .Should().BeEquivalentTo(
                survey.QuestionIds,
                opt => opt.WithStrictOrdering());
    }
}