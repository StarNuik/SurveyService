using Microsoft.AspNetCore.Mvc;
using SurveyService.Domain;
using SurveyService.Domain.Exception;
using SurveyService.Dto;

namespace SurveyService.Controllers;

[ApiController]
[Route(ApiPrefix)]
public class SurveyController(SurveyUsecase usecase) : ControllerBase
{
    public const string ApiPrefix = "/api/survey";

    [HttpPost("interview/new")]
    public async Task<PostInterviewResponse> PostNewInterview(PostInterviewRequest request)
    {
        var response = await usecase.NewInterview(request);
        return response;
    }

    [HttpGet("question/{id}")]
    public async Task<IActionResult> GetQuestion(long id)
    {
        // try
        // {
        //     var dto = await usecase.GetQuestion(id);
        //     return Ok(dto);
        // }
        // catch (NotFoundException)
        // {
        //     return BadRequest();
        // }
        throw new NotImplementedException();
    }
}