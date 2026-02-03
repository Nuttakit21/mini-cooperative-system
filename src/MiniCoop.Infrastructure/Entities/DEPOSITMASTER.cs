using MiniCoop.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Domain.Entities;

[Table("DEPOSITMASTER")]
public class DEPOSITMASTER
{
    [Key]
    [Column("ACCOUNT_NO")]
    [MaxLength(10)]
    public string ACCOUNT_NO { get; set; } = null!;

    [Required, MaxLength(10)]
    [Column("MEMBER_NO")]
    public string MEMBER_NO { get; set; } = null!;

    [Column("BALANCE", TypeName = "decimal(18,2)")]
    public decimal BALANCE { get; set; }

    [Required]
    [Column("OPEN_DATE")]
    public DateTime OPEN_DATE { get; set; }

    [Column("IS_ACTIVE")]
    public bool IS_ACTIVE { get; set; } = true;

    [ForeignKey("MEMBER_NO")]
    public MEMBERMASTER MEMBER { get; set; } = null!;
}