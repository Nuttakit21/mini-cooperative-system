using MiniCoop.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("TRANSACTION")]
public class TRANSACTION
{
    [Key]
    [Column("SEQ_NO")]
    public int SEQ_NO { get; set; }

    [Required, MaxLength(10)]
    [Column("ACCOUNT_NO")]
    public string ACCOUNT_NO { get; set; } = null!;

    [Required, MaxLength(3)]
    [Column("TRANSACTION_TYPE")]
    public string TRANSACTION_TYPE { get; set; } = null!;

    [Required]
    [Column("AMOUNT", TypeName = "decimal(18,2)")]
    public decimal AMOUNT { get; set; }

    [Required]
    [Column("BALANCE_AFTER", TypeName = "decimal(18,2)")]
    public decimal BALANCE_AFTER { get; set; }

    [Required]
    [Column("TRANSACTION_DATE")]
    public DateTime TRANSACTION_DATE { get; set; }

    [MaxLength(500)]
    [Column("REMARK")]
    public string? REMARK { get; set; }

    [Column("CREATED_AT")]
    public DateTime? CREATED_AT { get; set; }

    [ForeignKey("ACCOUNT_NO")]
    public DEPOSITMASTER DEPOSITMASTER { get; set; } = null!;
}