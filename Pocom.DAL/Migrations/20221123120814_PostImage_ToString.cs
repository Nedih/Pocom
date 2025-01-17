using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocom.DAL.Migrations
{
    public partial class PostImage_ToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d9148eb-43df-4ae0-901f-5e75ce63ee41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93f4a14c-d89c-4233-b644-fb92a363cc32");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c1ebabd-6745-4fc2-808d-48df8107736c", "538a039d-be6c-4c60-8b0f-f6cb614cbc26", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47e17bad-e591-4084-b31c-40c1e4859bd7", "96a1ac22-fe97-4ee4-b4e1-937162ce4c57", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c1ebabd-6745-4fc2-808d-48df8107736c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47e17bad-e591-4084-b31c-40c1e4859bd7");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Posts",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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
    }
}
