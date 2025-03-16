using FluentAssertions;
using Moq;
using SurveyService.Domain;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.UnitTests;

public class Test_SurveyService_GetQuestion
{
    [Fact]
    public async void HappyPath_HasCorrectTexts()
    {
        // Arrange
        var question = new Question
        {
            Id = 1,
            Text = "some text",
        };

        Answer[] answers = 
        [
            new Answer{Id = 1, QuestionId = question.Id, Text = "text 1"},
            new Answer{Id = 2, QuestionId = question.Id, Text = "text 2"},
            new Answer{Id = 3, QuestionId = question.Id, Text = "text 3"},
        ];
        
        var mock = new Mock<ISurveyRepository>();
        mock.Setup(repo => repo.GetQuestion(question.Id))
            .ReturnsAsync(question);
        mock.Setup(repo => repo.GetAnswersOfQuestion(question.Id))
            .ReturnsAsync(answers);

        var repo = mock.Object;
        var service = new Domain.SurveyService(repo);
        
        // Act
        var result = await service.GetQuestion(question.Id);
        
        // Assert
        result.Text
            .Should().BeEquivalentTo(question.Text);
        result.Answers
            .Should().BeEquivalentTo(answers.Select(
                from => new QuestionResponseAnswer
                {
                    Id = from.Id,
                    Text = from.Text,
                })
            );
    }
}