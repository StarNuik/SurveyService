namespace SurveyService.Dto;

public class GetAllSurveysResponse
{
    public GetAllSurveysResponseSurvey[] Surveys { get; set; }
}

public class GetAllSurveysResponseSurvey
{
    public long SurveyId { get; set; }
    public string Description { get; set; }
}