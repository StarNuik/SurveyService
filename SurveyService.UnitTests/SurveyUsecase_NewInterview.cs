using FluentAssertions;
using Moq;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.UnitTests;

public class SurveyUsecase_NewInterview
{
    [Fact]
    public async void HappyPath_ReturnIsCorrect()
    {
        // Arrange
        var userId = (long)1;
        var survey = new Survey
        {
            Id = 2,
            FirstQuestionId = 3,
        };
        var interview = new Interview
        {
            Id = 4,
            SurveyId = survey.Id,
            UserId = userId,
        };
        var question = new Question
        {
            Id = survey.FirstQuestionId,
            NextQuestionId = null,
            SurveyId = survey.Id,
            Text = "question text",
        };
        
        
        var mock = new Mock<ISurveyRepository>(MockBehavior.Strict);
        mock.Setup(repo => repo.GetSurvey(survey.Id))
            .ReturnsAsync(survey);
        mock.Setup(repo => repo.InsertInterview(It.Is<Interview>(
            interview => interview.SurveyId == survey.Id
                         && interview.UserId == userId
        ))).ReturnsAsync(interview);
        mock.Setup(repo => repo.GetQuestion(question.Id))
            .ReturnsAsync(question);
        mock.Setup(repo => repo.GetAnswersOfQuestion(question.Id))
            .ReturnsAsync([]);
        

        var repo = mock.Object;
        var usecase = new SurveyUsecase(repo);
        
        // Act
        var request = new PostInterviewRequest
        {
            SurveyId = survey.Id,
            UserId = userId,
        };
        var response = await usecase.NewInterview(request);
        
        // Assert
        response.InterviewId
            .Should().Be(interview.Id);
        response.FirstQuestion.Text
            .Should().Be(question.Text);
        
        mock.Verify(repo => repo.GetSurvey(It.IsAny<long>()), Times.Once);
        mock.Verify(repo => repo.InsertInterview(It.IsAny<Interview>()), Times.Once);
        mock.Verify(repo => repo.GetQuestion(It.IsAny<long>()), Times.Once);
        mock.Verify(repo => repo.GetAnswersOfQuestion(It.IsAny<long>()), Times.Once);
        mock.VerifyNoOtherCalls();
    }
}