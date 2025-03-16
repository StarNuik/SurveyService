using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SurveyService.Client;

namespace SurveyService.IntegrationTests;

public class SurveyService_GetQuestion(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private SurveyClient client = new SurveyClient(factory.CreateClient());
    
    [Fact]
    public async void SomethingWorks()
    {
        // Arrange
        // Act
        var result = await client.GetQuestion(1);
        
        // Assert
        result.Should()
            .NotBeNull();
    }
}