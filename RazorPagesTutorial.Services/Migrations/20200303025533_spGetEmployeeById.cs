using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesTutorial.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = 
                @"CREATE PROCEDURE [dbo].[spGetEmployeeById]
	                @Id int
                AS
	                SELECT * from dbo.Employees where Id = @Id
                RETURN 0";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure =
                @"DROP PROCEDURE [dbo].[spGetEmployeeById]";
	                

            migrationBuilder.Sql(procedure);
        }
    }
}
