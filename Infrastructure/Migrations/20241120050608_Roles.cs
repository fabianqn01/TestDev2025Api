using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_Role_RolesId",
                table: "ApplicationUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRole_Users_UsersId",
                table: "ApplicationUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserRole",
                table: "ApplicationUserRole");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "ApplicationUserRole",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserRole_UsersId",
                table: "UserRoles",
                newName: "IX_UserRoles_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RolesId",
                table: "UserRoles",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UsersId",
                table: "UserRoles",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RolesId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UsersId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "ApplicationUserRole");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UsersId",
                table: "ApplicationUserRole",
                newName: "IX_ApplicationUserRole_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserRole",
                table: "ApplicationUserRole",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_Role_RolesId",
                table: "ApplicationUserRole",
                column: "RolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRole_Users_UsersId",
                table: "ApplicationUserRole",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
