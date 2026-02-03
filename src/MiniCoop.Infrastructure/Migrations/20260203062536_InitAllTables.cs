using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniCoop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "APPLICATION",
                columns: table => new
                {
                    APPLICATION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    APPLICATION_CODE = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    APPLICATION_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ICON = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ORDER_NO = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION", x => x.APPLICATION_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MEMBERMASTER",
                columns: table => new
                {
                    MEMBER_NO = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FULL_NAME = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ID_CARD = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    STATUS = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OPEN_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBERMASTER", x => x.MEMBER_NO);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USERNAME = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PASSWORD_HASH = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FULL_NAME = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ROLE = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MENUGROUP",
                columns: table => new
                {
                    MENU_GROUP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    APPLICATION_ID = table.Column<int>(type: "int", nullable: false),
                    APPLICATION_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ICON = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ORDER_NO = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENUGROUP", x => x.MENU_GROUP_ID);
                    table.ForeignKey(
                        name: "FK_MENUGROUP_APPLICATION_APPLICATION_ID",
                        column: x => x.APPLICATION_ID,
                        principalTable: "APPLICATION",
                        principalColumn: "APPLICATION_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DEPOSITMASTER",
                columns: table => new
                {
                    ACCOUNT_NO = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MEMBER_NO = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BALANCE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OPEN_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPOSITMASTER", x => x.ACCOUNT_NO);
                    table.ForeignKey(
                        name: "FK_DEPOSITMASTER_MEMBERMASTER_MEMBER_NO",
                        column: x => x.MEMBER_NO,
                        principalTable: "MEMBERMASTER",
                        principalColumn: "MEMBER_NO",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MENUS",
                columns: table => new
                {
                    MENU_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MENU_GROUP_ID = table.Column<int>(type: "int", nullable: false),
                    MENU_NAME = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PATH = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IS_ACTIVE = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ORDER_NO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENUS", x => x.MENU_ID);
                    table.ForeignKey(
                        name: "FK_MENUS_MENUGROUP_MENU_GROUP_ID",
                        column: x => x.MENU_GROUP_ID,
                        principalTable: "MENUGROUP",
                        principalColumn: "MENU_GROUP_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TRANSACTION",
                columns: table => new
                {
                    SEQ_NO = table.Column<int>(type: "int", nullable: false),
                    ACCOUNT_NO = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TRANSACTION_TYPE = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BALANCE_AFTER = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TRANSACTION_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    REMARK = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATED_AT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    USERSID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACTION", x => new { x.ACCOUNT_NO, x.SEQ_NO });
                    table.ForeignKey(
                        name: "FK_TRANSACTION_DEPOSITMASTER_ACCOUNT_NO",
                        column: x => x.ACCOUNT_NO,
                        principalTable: "DEPOSITMASTER",
                        principalColumn: "ACCOUNT_NO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRANSACTION_USERS_USERSID",
                        column: x => x.USERSID,
                        principalTable: "USERS",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "APPLICATION",
                columns: new[] { "APPLICATION_ID", "APPLICATION_CODE", "APPLICATION_NAME", "ICON", "IS_ACTIVE", "ORDER_NO" },
                values: new object[,]
                {
                    { 1, "Member", "ระบบสมาชิก", "fa-solid fa-users", true, 1 },
                    { 2, "Deposit", "ระบบเงินฝาก", "fa-solid fa-piggy-bank", true, 2 }
                });

            migrationBuilder.InsertData(
                table: "MEMBERMASTER",
                columns: new[] { "MEMBER_NO", "FULL_NAME", "ID_CARD", "OPEN_DATE", "STATUS" },
                values: new object[,]
                {
                    { "M001", "สมชาย ใจดี", "1103700000011", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACTIVE" },
                    { "M002", "สมหญิง รวยดี", "1103700000022", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACTIVE" },
                    { "M003", "วิชัย ประหยัด", "1103700000033", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACTIVE" }
                });

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "ID", "CREATED_AT", "FULL_NAME", "PASSWORD_HASH", "ROLE", "USERNAME" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Admin", "$2a$11$3ozhA7tOFeQrgFDJtk5GBuQLO.NxRSKMWewa889QjyoGyL92oW90m", "Admin", "admin" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Staff User", "$2a$11$vVNwQwIDX6cN53SuXhjSIOnFyCZRFKh3iJFkny8T6HMsbTdikVMDS", "Staff", "staff" }
                });

            migrationBuilder.InsertData(
                table: "DEPOSITMASTER",
                columns: new[] { "ACCOUNT_NO", "BALANCE", "IS_ACTIVE", "MEMBER_NO", "OPEN_DATE" },
                values: new object[,]
                {
                    { "0000000001", 5000m, true, "M001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0000000002", 12000m, true, "M002", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "0000000003", 3000m, true, "M003", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MENUGROUP",
                columns: new[] { "MENU_GROUP_ID", "APPLICATION_ID", "APPLICATION_NAME", "ICON", "IS_ACTIVE", "ORDER_NO" },
                values: new object[,]
                {
                    { 1, 1, "ข้อมูลสมาชิก", "fa-solid fa-id-card", true, 1 },
                    { 2, 1, "รายงานสมาชิก", "fa-solid fa-file-lines", true, 2 },
                    { 3, 2, "บัญชีเงินฝาก", "fa-solid fa-book", true, 1 },
                    { 4, 2, "รายงานเงินฝาก", "fa-solid fa-chart-line", true, 2 }
                });

            migrationBuilder.InsertData(
                table: "MENUS",
                columns: new[] { "MENU_ID", "IS_ACTIVE", "MENU_GROUP_ID", "MENU_NAME", "ORDER_NO", "PATH" },
                values: new object[,]
                {
                    { 1, true, 1, "ทะเบียนสมาชิก", 1, "List" },
                    { 2, true, 1, "เพิ่มสมาชิก", 2, "Create" },
                    { 3, true, 1, "แก้ไขสมาชิก", 3, "Edit" },
                    { 4, true, 3, "เปิดบัญชีเงินฝาก", 1, "Open" },
                    { 5, true, 3, "ฝากเงิน", 2, "In" },
                    { 6, true, 3, "ถอนเงิน", 3, "Out" }
                });

            migrationBuilder.InsertData(
                table: "TRANSACTION",
                columns: new[] { "ACCOUNT_NO", "SEQ_NO", "AMOUNT", "BALANCE_AFTER", "CREATED_AT", "REMARK", "TRANSACTION_DATE", "TRANSACTION_TYPE", "USERSID" },
                values: new object[,]
                {
                    { "0000000001", 1, 5000m, 5000m, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "0000000002", 1, 12000m, 12000m, null, null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "0000000003", 1, 3000m, 3000m, null, null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "DEP", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DEPOSITMASTER_MEMBER_NO",
                table: "DEPOSITMASTER",
                column: "MEMBER_NO");

            migrationBuilder.CreateIndex(
                name: "IX_MENUGROUP_APPLICATION_ID",
                table: "MENUGROUP",
                column: "APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MENUS_MENU_GROUP_ID",
                table: "MENUS",
                column: "MENU_GROUP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACTION_USERSID",
                table: "TRANSACTION",
                column: "USERSID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MENUS");

            migrationBuilder.DropTable(
                name: "TRANSACTION");

            migrationBuilder.DropTable(
                name: "MENUGROUP");

            migrationBuilder.DropTable(
                name: "DEPOSITMASTER");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "APPLICATION");

            migrationBuilder.DropTable(
                name: "MEMBERMASTER");
        }
    }
}
