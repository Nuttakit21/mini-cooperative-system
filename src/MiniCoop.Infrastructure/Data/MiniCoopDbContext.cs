using Microsoft.EntityFrameworkCore;
using MiniCoop.Infrastructure.Entities;

namespace MiniCoop.Infrastructure.Data;

public class MiniCoopDbContext : DbContext
{
    public MiniCoopDbContext(DbContextOptions<MiniCoopDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Users> Users => Set<Users>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =========================
        // Member (สมาชิก)
        // =========================
        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("members");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.MemberNo).IsUnique();

            entity.Property(e => e.MemberNo).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.IdCard).IsRequired().HasMaxLength(13);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.OpenDate).IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        // =========================
        // User (เจ้าหน้าที่)
        // =========================
        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();

            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Role).HasMaxLength(20);
        });
        // USER (ADMIN) ข้อมูลตัวอย่าง
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
        });

        // =========================
        // Transaction (ทำรายการ)
        // =========================
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.TransactionType).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            // FK -> Member
            entity.HasOne(t => t.Member).WithMany(m => m.Transactions).HasForeignKey(t => t.MemberId).OnDelete(DeleteBehavior.Restrict);
            // FK -> User
            entity.HasOne(t => t.User).WithMany(u => u.Transactions).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}