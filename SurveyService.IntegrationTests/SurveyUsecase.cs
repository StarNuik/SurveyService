using FluentAssertions;
using SurveyService.Dto;

namespace SurveyService.IntegrationTests;

// TODO: remove these comments
// Arrange
// Act
// Assert

public class SurveyUsecase
{
    [Fact]
    public async void NewInterview_Success()
    {
        // Arrange
        var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var userId = 123; 
        var survey = await testRepo.InsertSurvey(new() { Description = "Test Survey" });
        
        var repo = testRepo.NewSurveyRepository();
        var usecase = new Domain.SurveyUsecase(repo);
        
        // Act
        var request = new PostInterviewRequest
        {
            SurveyId = survey.Id,
            UserId = userId,
        };
        var response = await usecase.NewInterview(request);
        
        // Assert
        var interview = await testRepo.SelectInterview();

        response.InterviewId
            .Should().Be(interview.Id);

        interview.SurveyId
            .Should().Be(survey.Id);
        interview.UserId
            .Should().Be(userId);
    }

    [Fact]
    public async void GetQuestion_Success()
    {
        // Arrange
        var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var survey = await testRepo.InsertSurvey(new() { Description = "Test Survey" });
        var question = await testRepo.InsertQuestion(new()
            { Description = "Test Question", Index = 3, SurveyId = survey.Id, });
        
        var repo = testRepo.NewSurveyRepository();
        var usecase = new Domain.SurveyUsecase(repo);
        
        // Act
        var request = new GetQuestionRequest
        {
            SurveyId = survey.Id,
            QuestionIndex = question.Index,
        };
        var response = await usecase.GetQuestion(request);
        
        // Assert
        // response.
    }
}