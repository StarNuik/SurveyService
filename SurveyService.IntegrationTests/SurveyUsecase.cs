using FluentAssertions;
using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.IntegrationTests;

[Collection("uses_postgres")]
public class SurveyUsecase
{
    [Fact]
    public async void NewInterview_Success()
    {
        // Arrange
        var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var userId = 123; 
        var survey = await testRepo.InsertSurvey(new()
        {
            Description = "Test Survey",
            QuestionIds = []
        });
        
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
        var usecase = new Domain.SurveyUsecase(repo);
        
        // Act
        var response = await usecase.GetQuestion(question.Id);
        
        // Assert
        response.Description
            .Should().Be(question.Description);
        response.Answers
            .Should().BeEquivalentTo(answers
                .Select(a => new GetQuestionResponseAnswer
                {
                    Description = a.Description,
                    Id = a.Id
                }));
    }

    [Fact]
    public async void SaveResult_Success()
    {
        // Arrange
        var testRepo = new TestSurveyRepository();
        await testRepo.Truncate();

        var userId = 123;
        var survey = await testRepo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = []
        });
        var interview = await testRepo.InsertInterview(new Interview
        {
            SurveyId = survey.Id,
            UserId = userId
        });
        var question = await testRepo.InsertQuestion(new Question
        {
            Description = "Test Question",
            SurveyId = survey.Id
        });
        var answer = await testRepo.InsertAnswer(new Answer
        {
            Description = "Answer 0",
            QuestionId = question.Id
        });
        
        var repo = testRepo.NewSurveyRepository();
        var usecase = new Domain.SurveyUsecase(repo);
        
        // Act
        var request = new PostResultRequest
        {
            AnswerId = answer.Id,
            InterviewId = interview.Id,
        };
        await usecase.SaveResult(request);
        
        // Assert
        var result = await testRepo.SelectResult();
        result.AnswerId
            .Should().Be(request.AnswerId);
        result.InterviewId
            .Should().Be(request.InterviewId);
    }
}