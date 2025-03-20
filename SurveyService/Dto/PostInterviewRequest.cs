using System.ComponentModel.DataAnnotations;

namespace SurveyService.Dto;

public class PostInterviewRequest
{
    [Required]
    public long UserId { get; set; }
    [Required]
    public long SurveyId { get; set; }
}