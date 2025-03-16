using Microsoft.AspNetCore.Mvc;
using SurveyService.Domain;
using SurveyService.Dto;

namespace SurveyService.Controllers;

[ApiController]
[Route("/api/survey")]
public class SurveyController : ControllerBase
{
    [HttpGet("question/{id}")]
    public async Task<QuestionResponse> GetQuestion(long id)
    {
        Console.WriteLine($"QuestionId: {id}");
        return new QuestionResponse();
    }
}