using System.Runtime.ConstrainedExecution;
using FluentMigrator;

namespace SurveyService.Migrations.Migrations;

[Migration(1)]
public class AddTables : Migration
{
    public override void Up()
    {
        Create.Table("survey")
            .WithIdColumn()
            .WithColumn("description").AsString().NotNullable();

        Create.Table("interview")
            .WithIdColumn()
            .WithColumn("userid").AsInt64().NotNullable()
            .WithIdReferenceColumn("survey");

        Create.Table("question")
            .WithIdColumn()
            .WithColumn("index").AsInt64().NotNullable()
            .WithColumn("description").AsString().NotNullable()
            .WithIdReferenceColumn("survey");

        Create.Table("answer")
            .WithIdColumn()
            .WithColumn("description").AsString().NotNullable()
            .WithIdReferenceColumn("question");

        Create.Table("result")
            .WithIdColumn()
            .WithIdReferenceColumn("interview")
            .WithIdReferenceColumn("answer");
    }
    
    public override void Down()
    {
        throw new NotImplementedException();
    }
    
    
}