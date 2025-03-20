using SurveyService.Domain.Entity;
using SurveyService.IntegrationTests;

namespace SurveyService.E2ETests;

public static class TestSurveyRepositoryExtension
{
    public static async Task PopulateWithTestData(this TestSurveyRepository repo)
    {
        for (var surveyIdx = 0; surveyIdx < 3; surveyIdx++)
        {
            var survey = await repo.InsertSurvey(new Survey
            {
                Description = $"Test Survey {surveyIdx}",
                QuestionIds = []
            });

            var questionIds = new long[3];
            for (var questionIdx = 0; questionIdx < 3; questionIdx++)
            {
                var question = await repo.InsertQuestion(new Question
                {
                    Description = $"Test Question {surveyIdx}.{questionIdx}",
                    SurveyId = survey.Id
                });
                questionIds[questionIdx] = question.Id;

                for (var answerIdx = 0; answerIdx < 3; answerIdx++)
                    await repo.InsertAnswer(new Answer
                    {
                        Description = $"Test Answer {surveyIdx}.{questionIdx}.{answerIdx}",
                        QuestionId = question.Id
                    });
            }

            await repo.UpdateSurveyQuestions(survey.Id, questionIds);
        }
    }
}