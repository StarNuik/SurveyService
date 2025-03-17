using FluentAssertions;
using Moq;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.UnitTests;

public class SurveyUsecase_GetQuestion
{
    [Fact]
    public async void HappyPath_HasCorrectTexts()
    {
        // Arrange
        var question = new Question
        {
            Id = 1,
            Text = "some text"
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
        var result = await usecase.GetQuestion(question.Id);

        // Assert
        result.Text
            .Should().BeEquivalentTo(question.Text);
        result.Answers
            .Should().BeEquivalentTo(answers.Select(
                from => new GetQuestionResponseAnswer
                {
                    Id = from.Id,
                    Text = from.Text
                })
            );
    }
}