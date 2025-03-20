using Microsoft.AspNetCore.Mvc;
using Npgsql;
using SurveyService.Domain;
using SurveyService.Dto;

namespace SurveyService.Controllers;

[ApiController]
[Route(ApiPrefix)]
public class SurveyController(SurveyUsecase usecase) : ControllerBase
{
    public const string ApiPrefix = "/api/survey";

    [HttpGet("all")]
    public async Task<IActionResult> GetAllSurveys()
    {
        throw new NotImplementedException();
    }

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

    [HttpPost("result")]
    public async Task<IActionResult> PostResult(PostResultRequest request)
    {
        try
        {
            await usecase.SaveResult(request);
            return Ok();
        }
        catch (PostgresException e)
        {
            // https://www.postgresql.org/docs/current/errcodes-appendix.html
            // Class 23 — Integrity Constraint Violation
            // foreign_key_violation
            if (e.SqlState == "23503") return BadRequest();
            throw e;
        }
    }
}