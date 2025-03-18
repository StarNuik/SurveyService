using System.Data;
using Dapper;
using FluentAssertions;
using Npgsql;
using SurveyService.Domain.Entity;

namespace SurveyService.IntegrationTests;

public class SurveyRepository
{
    
    [Fact]
    public async void GetSurvey_Success()
    {
        // Arrange
        using var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();
        
        var survey = await testRepo.InsertSurvey(new() {Description = "Test Survey"});
        
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
        
        var survey = await testRepo.InsertSurvey(new() {Description = "Test Survey"});

        var repo = testRepo.NewSurveyRepository();

        // Act
        var request = new Interview
        {
            SurveyId = survey.Id,
            UserId = 123,
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
        
        var survey = await testRepo.InsertSurvey(new() {Description = "Test Survey"});
        var question = await testRepo.InsertQuestion(new()
        {
            Description = "Test Question", Index = 25, SurveyId = survey.Id,
        });

        var repo = testRepo.NewSurveyRepository();
        
        // Act
        var response = await repo.GetQuestion(survey.Id, question.Index);
        
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
        
        var survey = await testRepo.InsertSurvey(new() {Description = "Test Survey"});
        var question = await testRepo.InsertQuestion(new()
        {
            Description = "Test Question", Index = 25, SurveyId = survey.Id,
        });

        var answers = new Answer[3];
        answers[0] = await testRepo.InsertAnswer(new() { Description = "Answer 0", QuestionId = question.Id });
        answers[1] = await testRepo.InsertAnswer(new() { Description = "Answer 1", QuestionId = question.Id });
        answers[2] = await testRepo.InsertAnswer(new() { Description = "Answer 2", QuestionId = question.Id });

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
        var survey = await testRepo.InsertSurvey(new() {Description = "Test Survey"});
        var question = await testRepo.InsertQuestion(new()
        {
            Description = "Test Question", Index = 25, SurveyId = survey.Id,
        });

        var answers = new Answer[3];
        answers[0] = await testRepo.InsertAnswer(new() { Description = "Answer 0", QuestionId = question.Id });
        answers[1] = await testRepo.InsertAnswer(new() { Description = "Answer 1", QuestionId = question.Id });
        answers[2] = await testRepo.InsertAnswer(new() { Description = "Answer 2", QuestionId = question.Id });

        var interview = await testRepo.InsertInterview(new() { UserId = userId, SurveyId = survey.Id });

        var repo = testRepo.NewSurveyRepository();
        
        // Act
        var request = new Result
        {
            InterviewId = interview.Id,
            AnswerId = answers[1].Id,
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