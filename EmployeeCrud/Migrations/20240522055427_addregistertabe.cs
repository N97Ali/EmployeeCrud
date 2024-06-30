using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeCrud.Migrations
{
    /// <inheritdoc />
    public partial class addregistertabe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "EmployeeModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeModel",
                table: "EmployeeModel",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeModel",
                table: "EmployeeModel");

            migrationBuilder.RenameTable(
                name: "EmployeeModel",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");
        }
    }
}
