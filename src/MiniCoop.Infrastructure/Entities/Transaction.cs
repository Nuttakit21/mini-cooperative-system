using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

public class Transaction
{
    public int SeqNo { get; set; }

    [Required, MaxLength(10)]
    public string MemberNo { get; set; } = null!;

    [Required, MaxLength(3)]
    public string TransactionType { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BalanceAfter { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Member Member { get; set; } = null!;
}