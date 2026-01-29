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
                name: "members",
                columns: table => new
                {
                    MemberNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCard = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.MemberNo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequiredRole = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "menu_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menu_roles_menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    SeqNo = table.Column<int>(type: "int", nullable: false),
                    MemberNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionType = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => new { x.MemberNo, x.SeqNo });
                    table.ForeignKey(
                        name: "FK_Transactions_members_MemberNo",
                        column: x => x.MemberNo,
                        principalTable: "members",
                        principalColumn: "MemberNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "members",
                columns: new[] { "MemberNo", "AccountBalance", "FullName", "IdCard", "OpenDate", "Status" },
                values: new object[,]
                {
                    { "M0001", 5000m, "สมชาย ใจดี", "1103700000011", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { "M0002", 12000m, "สมหญิง ใจงาม", "1103700000022", new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { "M0003", 3000m, "วิชัย รุ่งเรือง", "1103700000033", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { "M0004", 8000m, "สายฝน แสงดาว", "1103700000044", new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { "M0005", 15000m, "มนตรี กล้าหาญ", "1103700000055", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { "M0006", 2000m, "จันทร์เพ็ญ สุขใจ", "1103700000066", new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" }
                });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "Id", "Icon", "IsActive", "Name", "OrderNo", "Path", "RequiredRole", "SortOrder" },
                values: new object[,]
                {
                    { 1, "add", true, "ฝากเงิน", 1, "Deposit", "User", 0 },
                    { 2, "remove", true, "ถอนเงิน", 2, "Withdraw", "User", 0 },
                    { 3, "list", true, "ประวัติ", 3, "Transactions", "User", 0 },
                    { 4, "person", true, "ข้อมูลสมาชิก", 4, "Members", "User", 0 },
                    { 5, "people", true, "จัดการผู้ใช้", 5, "Users", "Admin", 0 },
                    { 6, "settings", true, "ตั้งค่าระบบ", 6, "Settings", "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "FullName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Admin", "$2a$11$1heyANQHqpa3hrQV9/3c9eVA0uS.yewrl8Bo7wjPnwqDix5sm3haO", "Admin", "admin" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Staff User", "$2a$11$ueHqS0hMvFVv.hDaDUj05ut2YwsX3R9FToBM56QY6jrhZYVjlEnGK", "Staff", "staff" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "MemberNo", "SeqNo", "Amount", "BalanceAfter", "CreatedAt", "Remark", "TransactionDate", "TransactionType", "UsersId" },
                values: new object[,]
                {
                    { "M0001", 1, 5000m, 5000m, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "เปิดบัญชี", new DateTime(2025, 1, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0001", 2, 1000m, 4000m, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ถอนเงิน", new DateTime(2025, 1, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), "WDL", null },
                    { "M0002", 1, 8000m, 8000m, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "ฝากครั้งแรก", new DateTime(2025, 1, 6, 10, 15, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0002", 2, 3000m, 5000m, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ถอนเงิน", new DateTime(2025, 1, 12, 11, 45, 0, 0, DateTimeKind.Unspecified), "WDL", null },
                    { "M0003", 1, 12000m, 12000m, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "เปิดบัญชี", new DateTime(2025, 1, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0003", 2, 2000m, 10000m, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ถอนเงิน", new DateTime(2025, 1, 15, 16, 20, 0, 0, DateTimeKind.Unspecified), "WDL", null },
                    { "M0004", 1, 3000m, 3000m, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "ฝากเงิน", new DateTime(2025, 1, 8, 13, 10, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0004", 2, 2000m, 5000m, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "ฝากเพิ่ม", new DateTime(2025, 1, 18, 10, 5, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0005", 1, 15000m, 15000m, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "เปิดบัญชี", new DateTime(2025, 1, 9, 9, 45, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0005", 2, 5000m, 10000m, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ถอนเงิน", new DateTime(2025, 1, 20, 15, 30, 0, 0, DateTimeKind.Unspecified), "WDL", null },
                    { "M0006", 1, 4000m, 4000m, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "ฝากเงิน", new DateTime(2025, 1, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), "DEP", null },
                    { "M0006", 2, 1000m, 3000m, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ถอนเงิน", new DateTime(2025, 1, 22, 14, 50, 0, 0, DateTimeKind.Unspecified), "WDL", null }
                });

            migrationBuilder.InsertData(
                table: "menu_roles",
                columns: new[] { "Id", "MenuId", "Role" },
                values: new object[,]
                {
                    { 1, 1, "User" },
                    { 2, 2, "User" },
                    { 3, 3, "User" },
                    { 4, 4, "User" },
                    { 5, 5, "Admin" },
                    { 6, 6, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_roles_MenuId",
                table: "menu_roles",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UsersId",
                table: "Transactions",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_roles");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
