using Microsoft.EntityFrameworkCore.Migrations;

namespace DepoOtomasyonu.DataAccess.Migrations
{
    public partial class removeCustomerCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCompany",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCompany",
                table: "Customers",
                type: "text",
                nullable: false);
        }
    }
}
