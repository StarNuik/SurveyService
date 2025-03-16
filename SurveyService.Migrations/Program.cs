using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using SurveyService.Migrations.Migrations;

// TODO: remove absolute path
const string dbPath = "/home/nikolai/Sync/Projects/SurveyService/test.db";

var builder = WebApplication.CreateBuilder();

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Postgres"))
        .ScanIn(typeof(AddTables).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();

using var scope = app.Services.CreateScope();

var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

