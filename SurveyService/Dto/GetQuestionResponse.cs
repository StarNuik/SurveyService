namespace SurveyService.Dto;

public class GetQuestionResponse
{
    public string Description;
    public GetQuestionResponseAnswer[] Answers;
}

public class GetQuestionResponseAnswer
{
    public long Id;
    public string Description;
}