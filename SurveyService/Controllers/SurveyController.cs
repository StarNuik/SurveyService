using Microsoft.AspNetCore.Mvc;
using SurveyService.Domain;
using SurveyService.Domain.Exception;

namespace SurveyService.Controllers;

[ApiController]
[Route(ApiPrefix)]
public class SurveyController(SurveyUsecase usecase) : ControllerBase
{
    public const string ApiPrefix = "/api/survey";

    [HttpGet("question/{id}")]
    public async Task<IActionResult> GetQuestion(long id)
    {
        try
        {
            var dto = await usecase.GetQuestion(id);
            return Ok(dto);
        }
        catch (NotFoundException)
        {
            return BadRequest();
        }
    }
}