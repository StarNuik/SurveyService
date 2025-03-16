using FluentMigrator;

namespace SurveyService.Migrations.Migrations;

[Migration(1)]
public class AddTables : Migration
{
    public override void Up()
    {
        Create.Table("Survey")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity();

        Create.Table("Question")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Text").AsString().NotNullable()
            .WithColumn("SurveyId").AsInt64().ForeignKey("Survey", "Id");

        Create.Table("Answer")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Text").AsString().NotNullable()
            .WithColumn("QuestionId").AsInt64().ForeignKey("Question", "Id");

        Create.Table("Result")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt64().NotNullable()
            // .WithColumn("SurveyId").AsInt64().ForeignKey("Survey", "Id")
            // .WithColumn("QuestionId").AsInt64().ForeignKey("Question", "Id")
            .WithColumn("AnswerId").AsInt64().ForeignKey("Answer", "Id");
    }
    
    public override void Down()
    {
        throw new NotImplementedException();
    }
}