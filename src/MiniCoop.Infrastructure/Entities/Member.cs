namespace MiniCoop.Infrastructure.Entities;

public class Member
{
    public int Id { get; set; }

    // เลขสมาชิก
    public string MemberNo { get; set; } = null!;

    // ชื่อ-นามสกุล
    public string FullName { get; set; } = null!;

    // เลขบัตรประชาชน
    public string IdCard { get; set; } = null!;

    // ACTIVE / SUSPENDED / CLOSED
    public string Status { get; set; } = "ACTIVE";

    // วันที่สมัคร
    public DateOnly OpenDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}