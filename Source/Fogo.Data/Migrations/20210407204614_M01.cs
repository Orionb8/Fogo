using Microsoft.EntityFrameworkCore.Migrations;

namespace Fogo.Data.Migrations {

    public partial class M01 : Migration {

        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "AdvertTypes",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_AdvertTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworks",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Url = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_SocialNetworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(32)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(256)", nullable: false),
                    IsPhoneConfirmed = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworkAdvertTypes",
                columns: table => new {
                    SocialNetworkId = table.Column<string>(type: "varchar(32)", nullable: false),
                    AdvertTypeId = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_SocialNetworkAdvertTypes", x => new { x.SocialNetworkId, x.AdvertTypeId });
                    table.ForeignKey(
                        name: "FK_SocialNetworkAdvertTypes_AdvertTypes_AdvertTypeId",
                        column: x => x.AdvertTypeId,
                        principalTable: "AdvertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocialNetworkAdvertTypes_SocialNetworks_SocialNetworkId",
                        column: x => x.SocialNetworkId,
                        principalTable: "SocialNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisers",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Advertisers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Executors",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Executors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Executors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new {
                    UserId = table.Column<string>(type: "varchar(32)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExecutorSocialNetworks",
                columns: table => new {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    ExecutorId = table.Column<string>(type: "varchar(32)", nullable: false),
                    SocialNetworkId = table.Column<string>(type: "varchar(32)", nullable: false),
                    Url = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_ExecutorSocialNetworks", x => x.Id);
                    table.UniqueConstraint("AK_ExecutorSocialNetworks_ExecutorId_SocialNetworkId", x => new { x.ExecutorId, x.SocialNetworkId });
                    table.ForeignKey(
                        name: "FK_ExecutorSocialNetworks_Executors_ExecutorId",
                        column: x => x.ExecutorId,
                        principalTable: "Executors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutorSocialNetworks_SocialNetworks_SocialNetworkId",
                        column: x => x.SocialNetworkId,
                        principalTable: "SocialNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisers_UserId",
                table: "Advertisers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Executors_UserId",
                table: "Executors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutorSocialNetworks_SocialNetworkId",
                table: "ExecutorSocialNetworks",
                column: "SocialNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialNetworkAdvertTypes_AdvertTypeId",
                table: "SocialNetworkAdvertTypes",
                column: "AdvertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Advertisers");

            migrationBuilder.DropTable(
                name: "ExecutorSocialNetworks");

            migrationBuilder.DropTable(
                name: "SocialNetworkAdvertTypes");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Executors");

            migrationBuilder.DropTable(
                name: "AdvertTypes");

            migrationBuilder.DropTable(
                name: "SocialNetworks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}