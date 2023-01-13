using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Orderdetailscoladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "OrderDetail",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_AddressId",
                table: "OrderDetail",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Address_AddressId",
                table: "OrderDetail",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Address_AddressId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_AddressId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "OrderDetail");
        }
    }
}
