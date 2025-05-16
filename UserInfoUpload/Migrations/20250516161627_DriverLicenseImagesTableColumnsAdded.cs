using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserInfoUpload.Migrations
{
    /// <inheritdoc />
    public partial class DriverLicenseImagesTableColumnsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDiscriminator",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endorsements",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpirationDate",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FederalCompliance",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssueDate",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuingJurisdiction",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganDonor",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Restrictions",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SocialSecurityNumber",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                schema: "dbo",
                table: "DrivingLicenseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "Class",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "DocumentDiscriminator",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "Endorsements",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "FederalCompliance",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "IssuingJurisdiction",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "OrganDonor",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "Restrictions",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "Sex",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNumber",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "dbo",
                table: "DrivingLicenseImages");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                schema: "dbo",
                table: "DrivingLicenseImages");
        }
    }
}
