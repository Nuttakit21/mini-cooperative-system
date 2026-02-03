using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("USERS")]
public class USERS
{
    [Key]
    [Column("ID")]
    public int ID { get; set; }

    [Required, MaxLength(50)]
    [Column("USERNAME")]
    public string USERNAME { get; set; } = null!;

    [Required, MaxLength(255)]
    [Column("PASSWORD_HASH")]
    public string PASSWORD_HASH { get; set; } = null!;

    [MaxLength(200)]
    [Column("FULL_NAME")]
    public string? FULL_NAME { get; set; }

    [Required, MaxLength(20)]
    [Column("ROLE")]
    public string ROLE { get; set; } = "User";

    [Column("CREATED_AT")]
    public DateTime? CREATED_AT { get; set; }

    public ICollection<TRANSACTION>? TRANSACTIONS { get; set; }
}