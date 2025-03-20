using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.IntegrationTests;
[Collection("uses_postgres")]
public class SurveyController_GetAllSurveys(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly SurveyClient client = new(factory.CreateClient());

    [Fact]
    public async Task HappyPath_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var surveys = new Survey[3];
        surveys[0] = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey 0",
            QuestionIds = [1, 2, 3]
        });
        surveys[1] = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey 1",
            QuestionIds = [4, 5, 6]
        });
        surveys[2] = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey 2",
            QuestionIds = [7, 8, 9]
        });
        
        // Act
        var response = await client.GetAllSurveys();
        
        // Assert
        response.Surveys
            .Should().BeEquivalentTo(surveys
                .Select(s => new GetAllSurveysResponseSurvey
                {
                    SurveyId = s.Id,
                    Description = s.Description
                }));
    }
}