using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Domain.Entities;

[Table("APPLICATION")]
public class APPLICATION
{
    [Key]
    [Column("APPLICATION_ID")]
    public int APPLICATION_ID { get; set; }

    [Required, MaxLength(15)]
    [Column("APPLICATION_CODE")]
    public string APPLICATION_CODE { get; set; } = null!;

    [Required, MaxLength(100)]
    [Column("APPLICATION_NAME")]
    public string APPLICATION_NAME { get; set; } = null!;

    [MaxLength(50)]
    [Column("ICON")]
    public string? ICON { get; set; }

    [Required]
    [Column("ORDER_NO")]
    public int ORDER_NO { get; set; }

    [Required]
    [Column("IS_ACTIVE")]
    public bool IS_ACTIVE { get; set; } = true;

    public ICollection<MENUGROUP> MenuGroups { get; set; } = new List<MENUGROUP>();
}
