using System.Runtime.ConstrainedExecution;
using FluentMigrator;

namespace SurveyService.Migrations.Migrations;

[Migration(1)]
public class AddTables : Migration
{
    public override void Up()
    {
        Create.Table("Survey")
            .WithIdColumn()
            .WithColumn("Description").AsString().NotNullable();

        Create.Table("Interview")
            .WithIdColumn()
            .WithColumn("UserId").AsInt64().NotNullable()
            .WithIdReferenceColumn("Survey");

        Create.Table("Question")
            .WithIdColumn()
            .WithColumn("Index").AsInt64().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithIdReferenceColumn("Survey");

        Create.Table("Answer")
            .WithIdColumn()
            .WithColumn("Description").AsString().NotNullable()
            .WithIdReferenceColumn("Question");

        Create.Table("Result")
            .WithIdColumn()
            .WithIdReferenceColumn("Interview")
            .WithIdReferenceColumn("Answer");
    }
    
    public override void Down()
    {
        throw new NotImplementedException();
    }
    
    
}