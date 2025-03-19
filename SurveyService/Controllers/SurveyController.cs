using Microsoft.AspNetCore.Mvc;
using SurveyService.Domain;
using SurveyService.Dto;

namespace SurveyService.Controllers;

[ApiController]
[Route(ApiPrefix)]
public class SurveyController(SurveyUsecase usecase) : ControllerBase
{
    public const string ApiPrefix = "/api/survey";

    [HttpPost("interview/new")]
    public async Task<IActionResult> PostNewInterview(PostInterviewRequest request)
    {
        try
        {
            var response = await usecase.NewInterview(request);
            return Ok(response);
        }
        catch (InvalidOperationException)
        {
            return BadRequest();
        }
    }

    [HttpGet("question/{id}")]
    public async Task<IActionResult> GetQuestion(long id)
    {
        try
        {
            var dto = await usecase.GetQuestion(id);
            return Ok(dto);
        }
        catch (InvalidOperationException)
        {
            return BadRequest();
        }
    }
}