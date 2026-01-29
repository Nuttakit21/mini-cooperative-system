using Microsoft.EntityFrameworkCore;
using MiniCoop.Infrastructure.Entities;

public class MiniCoopDbContext : DbContext
{
    public MiniCoopDbContext(DbContextOptions<MiniCoopDbContext> options) : base(options) { }

    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Users> Users => Set<Users>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // USERS
        // =========================
        modelBuilder.Entity<Users>().HasData(
            new Users
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1888"),
                FullName = "System Admin",
                Role = "Admin",
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new Users
            {
                Id = 2,
                Username = "staff",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1888"),
                FullName = "Staff User",
                Role = "Staff",
                CreatedAt = new DateTime(2026, 1, 1)
            }
        );

        // =========================
        // MENU
        // =========================
        modelBuilder.Entity<Menu>().HasData(
            new Menu { Id = 1, Name = "ฝากเงิน", Path = "Deposit", Icon = "add", OrderNo = 1, IsActive = true, RequiredRole = "User" },
            new Menu { Id = 2, Name = "ถอนเงิน", Path = "Withdraw", Icon = "remove", OrderNo = 2, IsActive = true, RequiredRole = "User" },
            new Menu { Id = 3, Name = "ประวัติ", Path = "Transactions", Icon = "list", OrderNo = 3, IsActive = true, RequiredRole = "User" },
            new Menu { Id = 4, Name = "ข้อมูลสมาชิก", Path = "Members", Icon = "person", OrderNo = 4, IsActive = true, RequiredRole = "User" },
            new Menu { Id = 5, Name = "จัดการผู้ใช้", Path = "Users", Icon = "people", OrderNo = 5, IsActive = true, RequiredRole = "Admin" },
            new Menu { Id = 6, Name = "ตั้งค่าระบบ", Path = "Settings", Icon = "settings", OrderNo = 6, IsActive = true, RequiredRole = "Admin" }
        );

        // =========================
        // MENUROLE
        // =========================
        modelBuilder.Entity<MenuRole>().HasData(
            new MenuRole { Id = 1, MenuId = 1, Role = "User" },
            new MenuRole { Id = 2, MenuId = 2, Role = "User" },
            new MenuRole { Id = 3, MenuId = 3, Role = "User" },
            new MenuRole { Id = 4, MenuId = 4, Role = "User" },
            new MenuRole { Id = 5, MenuId = 5, Role = "Admin" },
            new MenuRole { Id = 6, MenuId = 6, Role = "Admin" }
        );

        // =========================
        // MEMBERS
        // =========================
        modelBuilder.Entity<Member>().HasData(
            new Member { MemberNo = "M0001", FullName = "สมชาย ใจดี", IdCard = "1103700000011", Status = "Active", AccountBalance = 5000, OpenDate = new DateTime(2025, 1, 1) },
            new Member { MemberNo = "M0002", FullName = "สมหญิง ใจงาม", IdCard = "1103700000022", Status = "Active", AccountBalance = 12000, OpenDate = new DateTime(2025, 1, 5) },
            new Member { MemberNo = "M0003", FullName = "วิชัย รุ่งเรือง", IdCard = "1103700000033", Status = "Active", AccountBalance = 3000, OpenDate = new DateTime(2025, 1, 10) },
            new Member { MemberNo = "M0004", FullName = "สายฝน แสงดาว", IdCard = "1103700000044", Status = "Active", AccountBalance = 8000, OpenDate = new DateTime(2025, 1, 12) },
            new Member { MemberNo = "M0005", FullName = "มนตรี กล้าหาญ", IdCard = "1103700000055", Status = "Active", AccountBalance = 15000, OpenDate = new DateTime(2025, 1, 15) },
            new Member { MemberNo = "M0006", FullName = "จันทร์เพ็ญ สุขใจ", IdCard = "1103700000066", Status = "Active", AccountBalance = 2000, OpenDate = new DateTime(2025, 1, 20) }
        );

        // =========================
        // TRANSACTIONS
        // =========================
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => new { t.MemberNo, t.SeqNo });
            entity.HasOne(t => t.Member).WithMany().HasForeignKey(t => t.MemberNo).HasPrincipalKey(m => m.MemberNo);
        });
        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { MemberNo = "M0001", SeqNo = 1, TransactionType = "DEP", Amount = 5000, BalanceAfter = 5000, TransactionDate = new DateTime(2025, 1, 5, 9, 30, 0), Remark = "เปิดบัญชี", CreatedAt = new DateTime(2025, 1, 5) },
            new Transaction { MemberNo = "M0001", SeqNo = 2, TransactionType = "WDL", Amount = 1000, BalanceAfter = 4000, TransactionDate = new DateTime(2025, 1, 10, 14, 0, 0), Remark = "ถอนเงิน", CreatedAt = new DateTime(2025, 1, 10) },
            new Transaction { MemberNo = "M0002", SeqNo = 1, TransactionType = "DEP", Amount = 8000, BalanceAfter = 8000, TransactionDate = new DateTime(2025, 1, 6, 10, 15, 0), Remark = "ฝากครั้งแรก", CreatedAt = new DateTime(2025, 1, 6) },
            new Transaction { MemberNo = "M0002", SeqNo = 2, TransactionType = "WDL", Amount = 3000, BalanceAfter = 5000, TransactionDate = new DateTime(2025, 1, 12, 11, 45, 0), Remark = "ถอนเงิน", CreatedAt = new DateTime(2025, 1, 12) },
            new Transaction { MemberNo = "M0003", SeqNo = 1, TransactionType = "DEP", Amount = 12000, BalanceAfter = 12000, TransactionDate = new DateTime(2025, 1, 7, 9, 0, 0), Remark = "เปิดบัญชี", CreatedAt = new DateTime(2025, 1, 7) },
            new Transaction { MemberNo = "M0003", SeqNo = 2, TransactionType = "WDL", Amount = 2000, BalanceAfter = 10000, TransactionDate = new DateTime(2025, 1, 15, 16, 20, 0), Remark = "ถอนเงิน", CreatedAt = new DateTime(2025, 1, 15) },
            new Transaction { MemberNo = "M0004", SeqNo = 1, TransactionType = "DEP", Amount = 3000, BalanceAfter = 3000, TransactionDate = new DateTime(2025, 1, 8, 13, 10, 0), Remark = "ฝากเงิน", CreatedAt = new DateTime(2025, 1, 8) },
            new Transaction { MemberNo = "M0004", SeqNo = 2, TransactionType = "DEP", Amount = 2000, BalanceAfter = 5000, TransactionDate = new DateTime(2025, 1, 18, 10, 5, 0), Remark = "ฝากเพิ่ม", CreatedAt = new DateTime(2025, 1, 18) },
            new Transaction { MemberNo = "M0005", SeqNo = 1, TransactionType = "DEP", Amount = 15000, BalanceAfter = 15000, TransactionDate = new DateTime(2025, 1, 9, 9, 45, 0), Remark = "เปิดบัญชี", CreatedAt = new DateTime(2025, 1, 9) },
            new Transaction { MemberNo = "M0005", SeqNo = 2, TransactionType = "WDL", Amount = 5000, BalanceAfter = 10000, TransactionDate = new DateTime(2025, 1, 20, 15, 30, 0), Remark = "ถอนเงิน", CreatedAt = new DateTime(2025, 1, 20) },
            new Transaction { MemberNo = "M0006", SeqNo = 1, TransactionType = "DEP", Amount = 4000, BalanceAfter = 4000, TransactionDate = new DateTime(2025, 1, 11, 11, 0, 0), Remark = "ฝากเงิน", CreatedAt = new DateTime(2025, 1, 11) },
            new Transaction { MemberNo = "M0006", SeqNo = 2, TransactionType = "WDL", Amount = 1000, BalanceAfter = 3000, TransactionDate = new DateTime(2025, 1, 22, 14, 50, 0), Remark = "ถอนเงิน", CreatedAt = new DateTime(2025, 1, 22) }
        );
    }
}