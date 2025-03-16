using SurveyService.Domain;
using SurveyService.Infrastructure;

namespace SurveyService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        // services.AddSingleton<SurveyUsecase, >()
        services.AddSingleton<ISurveyRepository, SurveyRepository>();
        services.AddSingleton<SurveyUsecase>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}