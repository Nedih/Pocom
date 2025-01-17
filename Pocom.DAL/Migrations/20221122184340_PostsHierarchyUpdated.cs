using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocom.DAL.Migrations
{
    public partial class PostsHierarchyUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae8d8a33-c0d8-47e9-9ac6-f52092165451");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc6769f6-a965-4a35-b583-0f74ecc8fa09");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentPostId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4dce99cf-9c3a-42da-a179-9ff4de51e617", "dad1ca34-dae3-4c67-92a7-2c9759bfe199", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8817ef7-cd14-4a6c-9457-51a8e6da483f", "15bc855e-2a00-4f18-a01e-b040d3a098eb", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentPostId",
                table: "Posts",
                column: "ParentPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ParentPostId",
                table: "Posts",
                column: "ParentPostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ParentPostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ParentPostId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4dce99cf-9c3a-42da-a179-9ff4de51e617");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8817ef7-cd14-4a6c-9457-51a8e6da483f");

            migrationBuilder.DropColumn(
                name: "ParentPostId",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae8d8a33-c0d8-47e9-9ac6-f52092165451", "0055e714-746a-4683-b174-610868b63254", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bc6769f6-a965-4a35-b583-0f74ecc8fa09", "5ef2aa1a-642f-4acd-93f5-86f14ab544d1", "Admin", "ADMIN" });
        }
    }
}
