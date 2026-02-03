using Microsoft.EntityFrameworkCore;
using MiniCoop.Domain.Entities;
using MiniCoop.Infrastructure.Entities;

public class MiniCoopDbContext : DbContext
{
    public MiniCoopDbContext(DbContextOptions<MiniCoopDbContext> options) : base(options) { }

    public DbSet<APPLICATION> Application => Set<APPLICATION>();
    public DbSet<MENUGROUP> Menugroup => Set<MENUGROUP>();
    public DbSet<MENUS> Menus => Set<MENUS>();
    public DbSet<MEMBERMASTER> Members => Set<MEMBERMASTER>();
    public DbSet<USERS> USERS => Set<USERS>();
    public DbSet<TRANSACTION> Transactions => Set<TRANSACTION>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // USERS
        // =========================
        modelBuilder.Entity<USERS>().HasData(
            new USERS
            {
                ID = 1,
                USERNAME = "admin",
                PASSWORD_HASH = BCrypt.Net.BCrypt.HashPassword("1888"),
                FULL_NAME = "System Admin",
                ROLE = "Admin",
                CREATED_AT = new DateTime(2026, 1, 1)
            },
            new USERS
            {
                ID = 2,
                USERNAME = "staff",
                PASSWORD_HASH = BCrypt.Net.BCrypt.HashPassword("1888"),
                FULL_NAME = "Staff User",
                ROLE = "Staff",
                CREATED_AT = new DateTime(2026, 1, 1)
            }
        );

        // =========================
        // APPLICATION
        // =========================
        modelBuilder.Entity<APPLICATION>().HasData(
            new APPLICATION { APPLICATION_ID = 1, APPLICATION_CODE = "Member", APPLICATION_NAME = "ระบบสมาชิก", ICON = "fa-solid fa-users", ORDER_NO = 1, IS_ACTIVE = true },
            new APPLICATION { APPLICATION_ID = 2, APPLICATION_CODE = "Deposit", APPLICATION_NAME = "ระบบเงินฝาก", ICON = "fa-solid fa-piggy-bank", ORDER_NO = 2, IS_ACTIVE = true }
        );

        // =========================
        // MENUGROUP
        // =========================
        modelBuilder.Entity<MENUGROUP>().HasData(
            new MENUGROUP { MENU_GROUP_ID = 1, APPLICATION_ID = 1, APPLICATION_NAME = "ข้อมูลสมาชิก", ICON = "fa-solid fa-id-card", ORDER_NO = 1, IS_ACTIVE = true },
            new MENUGROUP { MENU_GROUP_ID = 2, APPLICATION_ID = 1, APPLICATION_NAME = "รายงานสมาชิก", ICON = "fa-solid fa-file-lines", ORDER_NO = 2, IS_ACTIVE = true },

            new MENUGROUP { MENU_GROUP_ID = 3, APPLICATION_ID = 2, APPLICATION_NAME = "บัญชีเงินฝาก", ICON = "fa-solid fa-book", ORDER_NO = 1, IS_ACTIVE = true },
            new MENUGROUP { MENU_GROUP_ID = 4, APPLICATION_ID = 2, APPLICATION_NAME = "รายงานเงินฝาก", ICON = "fa-solid fa-chart-line", ORDER_NO = 2, IS_ACTIVE = true }
        );


        // =========================
        // MENU
        // =========================
        modelBuilder.Entity<MENUS>().HasData(
            new MENUS { MENU_ID = 1, MENU_GROUP_ID = 1, MENU_NAME = "ทะเบียนสมาชิก", PATH = "List", ORDER_NO = 1, IS_ACTIVE = true },
            new MENUS { MENU_ID = 2, MENU_GROUP_ID = 1, MENU_NAME = "เพิ่มสมาชิก", PATH = "Create", ORDER_NO = 2, IS_ACTIVE = true },
            new MENUS { MENU_ID = 3, MENU_GROUP_ID = 1, MENU_NAME = "แก้ไขสมาชิก", PATH = "Edit", ORDER_NO = 3, IS_ACTIVE = true },

            new MENUS { MENU_ID = 4, MENU_GROUP_ID = 3, MENU_NAME = "เปิดบัญชีเงินฝาก", PATH = "Open", ORDER_NO = 1, IS_ACTIVE = true },
            new MENUS { MENU_ID = 5, MENU_GROUP_ID = 3, MENU_NAME = "ฝากเงิน", PATH = "In", ORDER_NO = 2, IS_ACTIVE = true },
            new MENUS { MENU_ID = 6, MENU_GROUP_ID = 3, MENU_NAME = "ถอนเงิน", PATH = "Out", ORDER_NO = 3, IS_ACTIVE = true }
        );


        // =========================
        // MEMBERS
        // =========================
        modelBuilder.Entity<MEMBERMASTER>().HasData(
            new MEMBERMASTER { MEMBER_NO = "M001", FULL_NAME = "สมชาย ใจดี", ID_CARD = "1103700000011", STATUS = "ACTIVE", OPEN_DATE = DateTime.Parse("2024-01-01") },
            new MEMBERMASTER { MEMBER_NO = "M002", FULL_NAME = "สมหญิง รวยดี", ID_CARD = "1103700000022", STATUS = "ACTIVE", OPEN_DATE = DateTime.Parse("2024-01-05") },
            new MEMBERMASTER { MEMBER_NO = "M003", FULL_NAME = "วิชัย ประหยัด", ID_CARD = "1103700000033", STATUS = "ACTIVE", OPEN_DATE = DateTime.Parse("2024-01-10") }
        );

        // =========================
        // DEPOSITMASTER
        // =========================
        modelBuilder.Entity<DEPOSITMASTER>().HasData(
            new DEPOSITMASTER { ACCOUNT_NO = "0000000001", MEMBER_NO = "M001", BALANCE = 5000, OPEN_DATE = DateTime.Parse("2024-01-01") },
            new DEPOSITMASTER { ACCOUNT_NO = "0000000002", MEMBER_NO = "M002", BALANCE = 12000, OPEN_DATE = DateTime.Parse("2024-01-05") },
            new DEPOSITMASTER { ACCOUNT_NO = "0000000003", MEMBER_NO = "M003", BALANCE = 3000, OPEN_DATE = DateTime.Parse("2024-01-10") }
        );


        // =========================
        // TRANSACTIONS
        // =========================
        modelBuilder.Entity<TRANSACTION>(entity =>
        {
            entity.HasKey(t => new { t.ACCOUNT_NO, t.SEQ_NO });
            entity.HasOne(t => t.DEPOSITMASTER).WithMany().HasForeignKey(t => t.ACCOUNT_NO).HasPrincipalKey(m => m.ACCOUNT_NO);
        });
        modelBuilder.Entity<TRANSACTION>().HasData(
            new TRANSACTION { SEQ_NO = 1, ACCOUNT_NO = "0000000001", TRANSACTION_TYPE = "DEP", AMOUNT = 5000, BALANCE_AFTER = 5000, TRANSACTION_DATE = DateTime.Parse("2024-01-01") },
            new TRANSACTION { SEQ_NO = 1, ACCOUNT_NO = "0000000002", TRANSACTION_TYPE = "DEP", AMOUNT = 12000, BALANCE_AFTER = 12000, TRANSACTION_DATE = DateTime.Parse("2024-01-05") },
            new TRANSACTION { SEQ_NO = 1, ACCOUNT_NO = "0000000003", TRANSACTION_TYPE = "DEP", AMOUNT = 3000, BALANCE_AFTER = 3000, TRANSACTION_DATE = DateTime.Parse("2024-01-10") }
        );

    }
}