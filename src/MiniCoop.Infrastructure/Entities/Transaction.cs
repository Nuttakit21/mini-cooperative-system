namespace MiniCoop.Infrastructure.Entities;

public class Transaction
{
    public long Id { get; set; }

    // FK -> Member
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;

    // FK -> User (เจ้าหน้าที่)
    public int UserId { get; set; }
    public Users User { get; set; } = null!;

    // DEPOSIT / WITHDRAW / PAYMENT
    public string TransactionType { get; set; } = null!;

    // จำนวนเงิน
    public decimal Amount { get; set; }

    // วันที่ทำรายการ
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    // หมายเหตุ
    public string? Remark { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}