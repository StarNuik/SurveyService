using Microsoft.AspNetCore.Mvc;
using Npgsql;
using SurveyService.Domain;
using SurveyService.Dto;

namespace SurveyService.Controllers;

[ApiController]
[Route(ApiPrefix)]
[Produces(jsonMime)]
public class SurveyController(SurveyUsecase usecase) : ControllerBase
{
    public const string ApiPrefix = "/api/survey";

    private const string jsonMime = "application/json";

    /// <summary>
    /// Получить все анкеты
    /// </summary>
    /// <response code="200">Список анкет с их id и описаниями</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(GetAllSurveysResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSurveys()
    {
        var response = await usecase.GetAllSurveys();
        return Ok(response);
    }

    /// <summary>
    /// Создать новое интервью (сессию прохождения анкеты)
    /// </summary>
    /// <param name="request">Json с id пользователя и id анкеты</param>
    /// <response code="200">Json с id интервью и id вопросов, которые нужно показать пользователю (по порядку)</response>
    /// <response code="400">Анкеты с таким id не существует</response>
    [HttpPost("interview/new")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PostInterviewResponse), StatusCodes.Status200OK)]
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

    /// <summary>
    /// Получить вопрос и его возможные ответы
    /// </summary>
    /// <param name="id">Id вопроса</param>
    /// <response code="200">Текст вопроса и список ответов, включающих: текст ответа и id</response>
    /// <response code="400">Вопрос с таким id не существует</response>
    [HttpGet("question/{id}")]
    [ProducesResponseType(typeof(GetQuestionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Сохранить ответ на вопрос
    /// </summary>
    /// <param name="request">Json c id интервью и id ответа</param>
    /// <response code="200">Ответ успешно сохранен</response>
    /// <response code="400">Ответа или интервью с таким id не существует</response>
    [HttpPost("result")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            throw;
        }
    }
}