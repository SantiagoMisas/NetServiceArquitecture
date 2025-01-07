using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_AddressId",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Address",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Address",
                newName: "Latitude");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Person",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Person_Id",
                table: "Address",
                column: "Id",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Person_Id",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Address",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Address",
                newName: "latitude");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Person",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                table: "Person",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
