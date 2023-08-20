using Microsoft.EntityFrameworkCore.Migrations;

namespace Nyma.Infra.Data.Migrations
{
    public partial class AddCustomerFeedBackTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_thingIDos",
                table: "thingIDos");

            migrationBuilder.RenameTable(
                name: "thingIDos",
                newName: "ThingIDos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThingIDos",
                table: "ThingIDos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerFeedBacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedBacks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerFeedBacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThingIDos",
                table: "ThingIDos");

            migrationBuilder.RenameTable(
                name: "ThingIDos",
                newName: "thingIDos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_thingIDos",
                table: "thingIDos",
                column: "Id");
        }
    }
}
