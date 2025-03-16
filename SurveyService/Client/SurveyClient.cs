using SurveyService.Dto;

namespace SurveyService.Client;


public class SurveyClient(HttpClient http)
{
    // TODO: make api strings global?
    const string ApiPrefix = "/api/survey";
    
    public async Task<QuestionResponse> GetQuestion(long questionId)
    {
        var response = await http.GetAsync($"{ApiPrefix}/question/{questionId}");
        var dto = await response.Content.ReadFromJsonAsync<QuestionResponse>();
        return dto;
    }

    public async Task PostResult()
    {
        throw new NotImplementedException();
    }
}