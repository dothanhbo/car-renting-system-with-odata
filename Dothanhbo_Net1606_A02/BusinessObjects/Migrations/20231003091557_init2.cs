using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarProducers_CarProducerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarProducerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarProducerId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerID",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ProducerID",
                table: "Cars",
                column: "ProducerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarProducers_ProducerID",
                table: "Cars",
                column: "ProducerID",
                principalTable: "CarProducers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarProducers_ProducerID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ProducerID",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "ProducerID",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarProducerId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarProducerId",
                table: "Cars",
                column: "CarProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarProducers_CarProducerId",
                table: "Cars",
                column: "CarProducerId",
                principalTable: "CarProducers",
                principalColumn: "Id");
        }
    }
}
