using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserInfoUpload.Migrations
{
    /// <inheritdoc />
    public partial class DriverLicenseInfoTableColumnsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "DrivingLicenseNumber",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "DrivingLicenseInfos");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserImages",
                newName: "UserImages",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "UserAttemptHistories",
                newName: "UserAttemptHistories",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "RealIdImages",
                newName: "RealIdImages",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DrivingLicenseInfos",
                newName: "DrivingLicenseInfos",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DrivingLicenseImages",
                newName: "DrivingLicenseImages",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "FrontDrivingLicenseImagePath",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "BackDrivingLicenseImagePath",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                newName: "SocialSecurityNumber");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IssueDate",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DateOfBirth",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentDiscriminator",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endorsements",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpirationDate",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FederalCompliance",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuingJurisdiction",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganDonor",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Restrictions",
                schema: "dbo",
                table: "DrivingLicenseInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                schema: "dbo",
                table: "DrivingLicenseInfos",
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
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "Class",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "DocumentDiscriminator",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "Endorsements",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "FederalCompliance",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "IssuingJurisdiction",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "OrganDonor",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "Restrictions",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.DropColumn(
                name: "Sex",
                schema: "dbo",
                table: "DrivingLicenseInfos");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserImages",
                schema: "dbo",
                newName: "UserImages");

            migrationBuilder.RenameTable(
                name: "UserAttemptHistories",
                schema: "dbo",
                newName: "UserAttemptHistories");

            migrationBuilder.RenameTable(
                name: "RealIdImages",
                schema: "dbo",
                newName: "RealIdImages");

            migrationBuilder.RenameTable(
                name: "DrivingLicenseInfos",
                schema: "dbo",
                newName: "DrivingLicenseInfos");

            migrationBuilder.RenameTable(
                name: "DrivingLicenseImages",
                schema: "dbo",
                newName: "DrivingLicenseImages");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "DrivingLicenseInfos",
                newName: "FrontDrivingLicenseImagePath");

            migrationBuilder.RenameColumn(
                name: "SocialSecurityNumber",
                table: "DrivingLicenseInfos",
                newName: "BackDrivingLicenseImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "DrivingLicenseInfos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "DrivingLicenseInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "DrivingLicenseInfos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "DrivingLicenseInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "DrivingLicenseInfos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DrivingLicenseInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DrivingLicenseNumber",
                table: "DrivingLicenseInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "DrivingLicenseInfos",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
