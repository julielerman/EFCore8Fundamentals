using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class ComboAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Authors",
                newName: "Addresses_ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_State",
                table: "Authors",
                newName: "Addresses_ShippingAddress_State");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Authors",
                newName: "Addresses_ShippingAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Authors",
                newName: "Addresses_ShippingAddress_City");

            migrationBuilder.AddColumn<string>(
                name: "Addresses_HomeAddress_City",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addresses_HomeAddress_PostalCode",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addresses_HomeAddress_State",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addresses_HomeAddress_Street",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Addresses_HomeAddress_City",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Addresses_HomeAddress_PostalCode",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Addresses_HomeAddress_State",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Addresses_HomeAddress_Street",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Addresses_ShippingAddress_Street",
                table: "Authors",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "Addresses_ShippingAddress_State",
                table: "Authors",
                newName: "Address_State");

            migrationBuilder.RenameColumn(
                name: "Addresses_ShippingAddress_PostalCode",
                table: "Authors",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Addresses_ShippingAddress_City",
                table: "Authors",
                newName: "Address_City");
        }
    }
}
