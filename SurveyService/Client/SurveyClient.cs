using System.Text;
using System.Text.Json;
using SurveyService.Controllers;
using SurveyService.Dto;

namespace SurveyService.Client;

public class SurveyClient(HttpClient http)
{
    private const string ApiPrefix = SurveyController.ApiPrefix;
    private const string jsonMime = "application/json";

    public async Task<GetAllSurveysResponse> GetAllSurveys()
    {
        var uri = $"{ApiPrefix}/all";
        var response = await http.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<GetAllSurveysResponse>();
        return dto;
    }

    public async Task<PostInterviewResponse> PostNewInterview(PostInterviewRequest request)
    {
        var uri = $"{ApiPrefix}/interview/new";
        var payload = JsonSerializer.Serialize(request);
        var response = await http.PostAsync(uri, new StringContent(payload, Encoding.UTF8, jsonMime));
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<PostInterviewResponse>();
        return dto;
    }

    public async Task<GetQuestionResponse> GetQuestion(long questionId)
    {
        var uri = $"{ApiPrefix}/question/{questionId}";
        var response = await http.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<GetQuestionResponse>();
        return dto;
    }

    public async Task PostResult(PostResultRequest request)
    {
        var uri = $"{ApiPrefix}/result";
        var payload = JsonSerializer.Serialize(request);
        var response = await http.PostAsync(uri, new StringContent(payload, Encoding.UTF8, jsonMime));
        response.EnsureSuccessStatusCode();
    }
}