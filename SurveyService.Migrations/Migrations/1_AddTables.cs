using System.Runtime.ConstrainedExecution;
using FluentMigrator;

namespace SurveyService.Migrations.Migrations;

[Migration(1)]
public class AddTables : Migration
{
    public override void Up()
    {
        Execute.Script("./Migrations/1_AddTables.sql");
    }
    
    public override void Down()
    {
        throw new NotImplementedException();
    }
    
    
}