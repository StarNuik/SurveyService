using SurveyService.Domain.Entity;
using SurveyService.Dto;

namespace SurveyService.Domain;

public static class Mapper
{
    public static GetQuestionResponse MakeQuestionResponse(Question question, Answer[] answers)
    {
        return new GetQuestionResponse
        {
            Description = question.Description,
            Answers = answers
                .Select(answer => new GetQuestionResponseAnswer
                {
                    Id = answer.Id,
                    Description = answer.Description
                }).ToArray()
        };
    }

    public static GetAllSurveysResponse MakeAllSurveysResponse(Survey[] surveys)
    {
        return new GetAllSurveysResponse
        {
            Surveys = surveys
                .Select(survey => new GetAllSurveysResponseSurvey
                {
                    SurveyId = survey.Id,
                    Description = survey.Description
                }).ToArray()
        };
    }
}