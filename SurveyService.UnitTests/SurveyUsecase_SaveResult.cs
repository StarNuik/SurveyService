using Moq;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.UnitTests;

public class SurveyUsecase_SaveResult
{
    [Fact]
    public async void HappyPath_InsertResultIsCalled()
    {
        // Arrange
        var request = new PostResultRequest
        {
            AnswerId = 1,
            InterviewId = 2,
        };
        
        var mock = new Mock<ISurveyRepository>(MockBehavior.Strict);
        mock.Setup(repo => repo.InsertResult(It.Is<Result>(
            result => result.AnswerId == request.AnswerId
                      && result.InterviewId == request.InterviewId
            ))).Returns(Task.CompletedTask);

        var repo = mock.Object;
        var usecase = new SurveyUsecase(repo);
        
        // Act
        await usecase.SaveResult(request);
        
        // Assert
        mock.Verify(repo => repo.InsertResult(It.IsAny<Result>()), Times.Once);
        mock.VerifyNoOtherCalls();
    }
}