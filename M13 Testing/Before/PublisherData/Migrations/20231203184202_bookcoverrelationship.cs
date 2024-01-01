using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class bookcoverrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Covers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1,
                column: "BookId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 2,
                column: "BookId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 3,
                column: "BookId",
                value: 1);

            migrationBuilder.UpdateData(table: "Covers",
                            keyColumn: "CoverId", keyValue: 4,
                            column: "BookId", value: 7);
            migrationBuilder.UpdateData(table: "Covers",
                            keyColumn: "CoverId", keyValue: 5,
                            column: "BookId", value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Covers_BookId",
                table: "Covers",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_Books_BookId",
                table: "Covers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_Books_BookId",
                table: "Covers");

            migrationBuilder.DropIndex(
                name: "IX_Covers_BookId",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Covers");
        }
    }
}
