using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GMS.Data.Migrations
{
    public partial class initdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
               name: "Address",
               table: "AspNetUsers",
               maxLength: 200,
               nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndivType",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfIssue",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RolePersonal",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrgAddress",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AreaConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaConfig", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "TreeCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ScientificName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    Url = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeCatalog", x => x.Id);
                });





            migrationBuilder.CreateTable(
                name: "AccessFuncRoleOrg",
                columns: table => new
                {
                    UserID = table.Column<string>(maxLength: 200, nullable: false),
                    FunctionName = table.Column<string>(maxLength: 150, nullable: false),
                    View = table.Column<bool>(nullable: true),
                    Delete = table.Column<bool>(nullable: true),
                    Edit = table.Column<bool>(nullable: true),
                    Add = table.Column<bool>(nullable: true),
                    Import = table.Column<bool>(nullable: true),
                    Export = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessFuncRoleOrg", x => new { x.UserID, x.FunctionName });
                    table.UniqueConstraint("AK_AccessFuncRoleOrg_FunctionName_UserID", x => new { x.FunctionName, x.UserID });
                    table.ForeignKey(
                        name: "FK_AccessFuncRoleOrg_Individual",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });









            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 200, nullable: false),
                    UserRole = table.Column<long>(nullable: false),
                    DecentralizationRole = table.Column<long>(nullable: false),
                    TreeCatalogRole = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Role_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessFuncRoleOrg");

         

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "TreeCatalog");

          

            migrationBuilder.DropTable(
                name: "AreaConfig");
            migrationBuilder.DropColumn(
               name: "Address",
               table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IndivType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlaceOfIssue",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
