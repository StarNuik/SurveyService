using SurveyService.Controllers;
using SurveyService.Dto;

namespace SurveyService.Client;


public class SurveyClient(HttpClient http)
{
    private const string ApiPrefix = SurveyController.ApiPrefix;
    
    public async Task<QuestionResponse> GetQuestion(long questionId)
    {
        var response = await http.GetAsync($"{ApiPrefix}/question/{questionId}");
        response.EnsureSuccessStatusCode();
        
        var dto = await response.Content.ReadFromJsonAsync<QuestionResponse>();
        return dto;
    }

    public async Task PostResult()
    {
        throw new NotImplementedException();
    }
}