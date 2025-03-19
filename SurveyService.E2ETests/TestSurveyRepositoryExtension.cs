using SurveyService.Domain.Entity;
using SurveyService.IntegrationTests;

namespace SurveyService.E2ETests;

public static class TestSurveyRepositoryExtension
{
    public static async Task<Survey> PopulateWithTestData(this TestSurveyRepository repo)
    {
        var survey = await repo.InsertSurvey(new Survey
        {
            Description = "Test Survey",
            QuestionIds = [],
        });

        var questions = new Question[3];
        for (var questionIdx = 0; questionIdx < 3; questionIdx++)
        {
            questions[questionIdx] = await repo.InsertQuestion(new Question
            {
                Description = $"Test Question {questionIdx}",
                SurveyId = survey.Id,
            });
        }

        survey = await repo.UpdateSurveyQuestions(survey.Id, questions
            .Select(q => q.Id).ToArray());

        foreach (var questionId in survey.QuestionIds)
            for (var answerIdx = 0; answerIdx < 3; answerIdx++)
            {
                await repo.InsertAnswer(new Answer
                {
                    Description = $"Test Answer {questionId}.{answerIdx}",
                    QuestionId = questionId,
                });
            }

        return survey;
    }
}