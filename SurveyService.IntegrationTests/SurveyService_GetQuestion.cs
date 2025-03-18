using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;

namespace SurveyService.IntegrationTests;

public class SurveyService_GetQuestion(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly SurveyClient client = new(factory.CreateClient());

    [Fact]
    public async void IncorrectQuestionId_Error()
    {
        // Arrange
        // Act
        // var call = async () => await client.GetQuestion(-1);
        //
        // // Assert
        // await call.Should()
        //     .ThrowAsync<HttpRequestException>();
    }

    // [Fact]
    // public async void CorrectQuestionId_DataIsCorrect()
    // {
    //     // Arrange
    //     // testRepo.AddData
    //     // Act
    //     var response = client.GetQuestion(1);
    //     
    //     // Assert
    // }
}