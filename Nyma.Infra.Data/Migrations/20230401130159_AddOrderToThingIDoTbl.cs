using Microsoft.EntityFrameworkCore.Migrations;

namespace Nyma.Infra.Data.Migrations
{
    public partial class AddOrderToThingIDoTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "thingIDos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "thingIDos");
        }
    }
}
