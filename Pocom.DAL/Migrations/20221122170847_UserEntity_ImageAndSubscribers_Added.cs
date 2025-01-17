using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocom.DAL.Migrations
{
    public partial class UserEntity_ImageAndSubscribers_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64a2c58b-add5-4c03-b286-1a63b65dd65f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f90d3e6a-8656-4835-8bee-089204953802");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAccountId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae8d8a33-c0d8-47e9-9ac6-f52092165451", "0055e714-746a-4683-b174-610868b63254", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc6769f6-a965-4a35-b583-0f74ecc8fa09", "5ef2aa1a-642f-4acd-93f5-86f14ab544d1", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserAccountId",
                table: "AspNetUsers",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserAccountId",
                table: "AspNetUsers",
                column: "UserAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserAccountId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserAccountId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae8d8a33-c0d8-47e9-9ac6-f52092165451");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc6769f6-a965-4a35-b583-0f74ecc8fa09");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64a2c58b-add5-4c03-b286-1a63b65dd65f", "342aedcc-3d7b-40c1-96e2-7867cfbad218", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f90d3e6a-8656-4835-8bee-089204953802", "679629f7-3cd6-4309-8ad0-c1a5f4576a72", "User", "USER" });
        }
    }
}
