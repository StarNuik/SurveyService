using FluentAssertions;
using SurveyService.Domain.Entity;

namespace SurveyService.IntegrationTests;

[Collection("uses_postgres")]
public class SurveyRepository
{
    [Fact]
    public async void GetSurvey_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var survey = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = [1, 2]
        });

        var repo = testRepo.NewSurveyRepository();

        // Act
        var response = await repo.GetSurvey(survey.Id);

        // Assert
        response
            .Should().BeEquivalentTo(survey);
    }

    [Fact]
    public async void InsertInterview_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var survey = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = []
        });

        var repo = testRepo.NewSurveyRepository();

        // Act
        var request = new Interview
        {
            SurveyId = survey.Id,
            UserId = 123
        };
        var response = await repo.InsertInterview(request);

        // Assert
        var interview = await testRepo.SelectInterview();
        response
            .Should().BeEquivalentTo(interview);
    }

    [Fact]
    public async void GetQuestion_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();
        
        var survey = await testRepo.InsertSurvey(new()
        {
            Description = "Test Survey",
            QuestionIds = [],
        });
        var question = await testRepo.InsertQuestion(new()
        {
            Description = "Test Question", SurveyId = survey.Id,
        });
        
        var repo = testRepo.NewSurveyRepository();
        
        // Act
        var response = await repo.GetQuestion(question.Id);
        
        // Assert
        response
            .Should().BeEquivalentTo(question);
    }

    [Fact]
    public async void GetAnswersOfQuestion_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var survey = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = []
        });
        var question = await testRepo.InsertQuestion(new Question
        {
            Description = "Test Question",
            SurveyId = survey.Id
        });

        var answers = new Answer[3];
        answers[0] = await testRepo.InsertAnswer(new Answer { Description = "Answer 0", QuestionId = question.Id });
        answers[1] = await testRepo.InsertAnswer(new Answer { Description = "Answer 1", QuestionId = question.Id });
        answers[2] = await testRepo.InsertAnswer(new Answer { Description = "Answer 2", QuestionId = question.Id });

        var repo = testRepo.NewSurveyRepository();

        // Act
        var response = await repo.GetAnswersOfQuestion(question.Id);

        // Assert
        response
            .Should().BeEquivalentTo(answers);
    }

    [Fact]
    public async void InsertResult_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var userId = 123;
        var survey = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = []
        });
        var question = await testRepo.InsertQuestion(new Question
        {
            Description = "Test Question",
            SurveyId = survey.Id
        });

        var answers = new Answer[3];
        answers[0] = await testRepo.InsertAnswer(new Answer { Description = "Answer 0", QuestionId = question.Id });
        answers[1] = await testRepo.InsertAnswer(new Answer { Description = "Answer 1", QuestionId = question.Id });
        answers[2] = await testRepo.InsertAnswer(new Answer { Description = "Answer 2", QuestionId = question.Id });

        var interview = await testRepo.InsertInterview(new Interview { UserId = userId, SurveyId = survey.Id });

        var repo = testRepo.NewSurveyRepository();

        // Act
        var request = new Result
        {
            InterviewId = interview.Id,
            AnswerId = answers[1].Id
        };
        await repo.InsertResult(request);

        // Assert
        var result = await testRepo.SelectResult();
        result.AnswerId
            .Should().Be(request.AnswerId);
        result.InterviewId
            .Should().Be(request.InterviewId);
    }
}