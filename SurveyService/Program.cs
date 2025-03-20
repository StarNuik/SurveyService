using System.Data;
using System.Reflection;
using Microsoft.Extensions.Options;
using Npgsql;
using SurveyService.Controllers;
using SurveyService.Domain;
using SurveyService.Infrastructure;

namespace SurveyService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        services.AddSingleton<Func<IDbConnection>>(() =>
        {
            var connectionString = builder.Configuration.GetConnectionString("Postgres");
            return new NpgsqlConnection(connectionString);
        });
        services.AddSingleton<ISurveyRepository, SurveyRepository>();
        services.AddSingleton<SurveyUsecase>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}