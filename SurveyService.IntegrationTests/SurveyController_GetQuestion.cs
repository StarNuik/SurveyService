using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.IntegrationTests;

[Collection("uses_postgres")]
public class SurveyController_GetQuestion(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly SurveyClient client = new(factory.CreateClient());

    [Fact]
    public async Task IncorrectQuestionId_BadRequest()
    {
        // Arrange
        var repo = new TestSurveyRepository();
        await repo.Truncate();
        
        // Act
        var call = async () => await client.GetQuestion(-1);
        
        // Assert
        await call
            .Should().ThrowAsync<HttpRequestException>()
            .WithMessage("*400*");
    }

    [Fact]
    public async Task HappyPath_Success()
    {
        // Arrange
        var repo = new TestSurveyRepository();
        await repo.Truncate();

        var survey = await repo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = [],
        });
        var question = await repo.InsertQuestion(new Question
        {
            Description = "Test Question",
            SurveyId = survey.Id,
        });
        var answers = new Answer[3];
        answers[0] = await repo.InsertAnswer(new Answer
        {
            Description = "Test Answer 0",
            QuestionId = question.Id,
        });
        answers[1] = await repo.InsertAnswer(new Answer
        {
            Description = "Test Answer 1",
            QuestionId = question.Id,
        });
        answers[2] = await repo.InsertAnswer(new Answer
        {
            Description = "Test Answer 2",
            QuestionId = question.Id,
        });
        
        // Act
        var response = await client.GetQuestion(question.Id);

        // Assert
        response.Description
            .Should().Be(question.Description);
        response.Answers
            .Should().BeEquivalentTo(answers
                .Select(a => new GetQuestionResponseAnswer
                {
                    Id = a.Id,
                    Description = a.Description
                }));
    }
}