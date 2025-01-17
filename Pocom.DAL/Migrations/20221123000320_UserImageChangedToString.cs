using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocom.DAL.Migrations
{
    public partial class UserImageChangedToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22486ea0-1c61-4210-a2e6-46fd4d6858b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9b74194-d63b-4e1d-afe8-40543e1d185d");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d9148eb-43df-4ae0-901f-5e75ce63ee41", "0b59dd38-d67d-4666-a80b-213ab1562e50", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93f4a14c-d89c-4233-b644-fb92a363cc32", "9a347614-4553-4226-9979-40bae915fce6", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d9148eb-43df-4ae0-901f-5e75ce63ee41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93f4a14c-d89c-4233-b644-fb92a363cc32");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "22486ea0-1c61-4210-a2e6-46fd4d6858b4", "823e13e9-075d-4710-b775-84d77885abfa", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c9b74194-d63b-4e1d-afe8-40543e1d185d", "2ae0bb8a-e16b-4478-91cd-681d2acbf56a", "Admin", "ADMIN" });
        }
    }
}
