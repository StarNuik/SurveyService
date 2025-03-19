using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;
using SurveyService.Dto;
using SurveyService.IntegrationTests;

namespace SurveyService.E2ETests;

[Collection("uses_postgres")]
public class SurveyService(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly SurveyClient client = new(factory.CreateClient());

    [Fact]
    public async Task BaseScenario_Success()
    {
        // Arrange
        using var repo = new TestSurveyRepository();
        await repo.Truncate();

        var survey = await repo.PopulateWithTestData();

        var userId = 1234;

        // Act
        var interviewRequest = new PostInterviewRequest
        {
            SurveyId = survey.Id,
            UserId = 1234
        };
        var interview = await client.PostNewInterview(interviewRequest);

        foreach (var questionId in interview.QuestionIds)
        {
            var question = await client.GetQuestion(questionId);

            var result = new PostResultRequest
            {
                InterviewId = interview.InterviewId,
                AnswerId = question.Answers.RandomElement().Id
            };
            await client.PostResult(result);
        }

        // Assert
        // no exceptions == success
    }
}