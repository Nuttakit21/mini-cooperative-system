using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("MEMBERMASTER")]
public class MEMBERMASTER
{
    [Key]
    [Required, MaxLength(10)]
    [Column("MEMBER_NO")]
    public string MEMBER_NO { get; set; } = null!;

    [Required, MaxLength(200)]
    [Column("FULL_NAME")]
    public string FULL_NAME { get; set; } = null!;

    [Required, MaxLength(13)]
    [Column("ID_CARD")]
    public string ID_CARD { get; set; } = null!;

    [Required, MaxLength(20)]
    [Column("STATUS")]
    public string STATUS { get; set; } = null!;

    public DateTime OPEN_DATE { get; set; }
}