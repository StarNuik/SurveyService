namespace SurveyService.Dto;

public class GetQuestionResponse
{
    public string Description { get; set; }
    public GetQuestionResponseAnswer[] Answers { get; set; }
}

public class GetQuestionResponseAnswer
{
    public long Id { get; set; }
    public string Description { get; set; }
}