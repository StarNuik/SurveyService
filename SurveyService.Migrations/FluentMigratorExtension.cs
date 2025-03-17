using FluentMigrator.Builders.Create.Table;

namespace SurveyService.Migrations;

public static class FluentMigratorExtension
{
    public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
    {
        return tableWithColumnSyntax
            .WithColumn("Id")
            .AsInt64()
            .NotNullable()
            .PrimaryKey()
            .Identity();
    }
    
    public static ICreateTableColumnOptionOrWithColumnSyntax WithIdReferenceColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax, string referencedTable)
    {
        return tableWithColumnSyntax
                .WithColumn(referencedTable + "Id")
                .AsInt64()
                .NotNullable()
                .ForeignKey(referencedTable, "Id");
    }
}