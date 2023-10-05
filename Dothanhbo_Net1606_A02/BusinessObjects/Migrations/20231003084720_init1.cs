using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarProducerId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerID",
                table: "Reviews",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarProducerId",
                table: "Cars",
                column: "CarProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_CustomerID",
                table: "CarRentals",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Cars_CarID",
                table: "CarRentals",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Customers_CustomerID",
                table: "CarRentals",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarProducers_CarProducerId",
                table: "Cars",
                column: "CarProducerId",
                principalTable: "CarProducers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cars_CarID",
                table: "Reviews",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerID",
                table: "Reviews",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Cars_CarID",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Customers_CustomerID",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarProducers_CarProducerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cars_CarID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustomerID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarProducerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_CustomerID",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "CarProducerId",
                table: "Cars");
        }
    }
}
