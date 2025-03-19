using FluentMigrator;

namespace SurveyService.Migrations.Migrations;

[Migration(2)]
public class StagingData : Migration
{
    public override void Up()
    {
        Execute.Script("./Migrations/2_StagingData.sql");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}