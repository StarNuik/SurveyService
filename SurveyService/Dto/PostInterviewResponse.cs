namespace SurveyService.Dto;

public class PostInterviewResponse
{
    public long InterviewId { get; set; }
    public long[] QuestionIds { get; set; }
}