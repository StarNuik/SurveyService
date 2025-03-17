using FluentAssertions;
using Moq;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.UnitTests;

public class SurveyUsecase_GetQuestion
{
    [Fact]
    public async void HappyPath_DataIsCorrect()
    {
        // Arrange
        var question = new Question
        {
            Id = 1,
            NextQuestionId = 2,
            Text = "question text"
        };

        Answer[] answers =
        [
            new() { Id = 1, QuestionId = question.Id, Text = "text 1" },
            new() { Id = 2, QuestionId = question.Id, Text = "text 2" },
            new() { Id = 3, QuestionId = question.Id, Text = "text 3" }
        ];

        var mock = new Mock<ISurveyRepository>();
        mock.Setup(repo => repo.GetQuestion(question.Id))
            .ReturnsAsync(question);
        mock.Setup(repo => repo.GetAnswersOfQuestion(question.Id))
            .ReturnsAsync(answers);

        var repo = mock.Object;
        var usecase = new SurveyUsecase(repo);

        // Act
        var response = await usecase.GetQuestion(question.Id);

        // Assert
        mock.Verify(repo => repo.GetQuestion(question.Id), Times.Once);
        mock.Verify(repo => repo.GetAnswersOfQuestion(question.Id), Times.Once);
        
        response.Text
            .Should().Be(question.Text);
        response.HasNextQuestion
            .Should().Be(true);
        response.NextQuestionId
            .Should().Be(question.NextQuestionId.Value);
        response.Answers
            .Should().BeEquivalentTo(answers.Select(
                from => new GetQuestionResponseAnswer
                {
                    Id = from.Id,
                    Text = from.Text
                })
            );
    }
    
    [Fact]
    public async void NullNextQuestion_Success()
    {
        // Arrange
        var question = new Question
        {
            Id = 1,
            NextQuestionId = null,
            Text = "question text"
        };

        var mock = new Mock<ISurveyRepository>();
        mock.Setup(repo => repo.GetQuestion(question.Id))
            .ReturnsAsync(question);
        mock.Setup(repo => repo.GetAnswersOfQuestion(question.Id))
            .ReturnsAsync([]);

        var repo = mock.Object;
        var usecase = new SurveyUsecase(repo);

        // Act
        var response = await usecase.GetQuestion(question.Id);

        // Assert
        mock.Verify(repo => repo.GetQuestion(question.Id), Times.Once);
        mock.Verify(repo => repo.GetAnswersOfQuestion(question.Id), Times.Once);
        
        response.Text
            .Should().Be(question.Text);
        response.HasNextQuestion
            .Should().Be(false);
        response.NextQuestionId
            .Should().Be(question.NextQuestionId.GetValueOrDefault());
        response.Answers
            .Should().BeEmpty();
    }
}