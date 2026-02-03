using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Domain.Entities;

[Table("MENUS")]
public class MENUS
{
    [Key]
    [Column("MENU_ID")]
    public int MENU_ID { get; set; }

    [Required]
    [Column("MENU_GROUP_ID")]
    public int MENU_GROUP_ID { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("MENU_NAME")]
    public string MENU_NAME { get; set; } = null!;

    [MaxLength(200)]
    [Column("PATH")]
    public string? PATH { get; set; }

    [Required]
    [Column("IS_ACTIVE")]
    public bool IS_ACTIVE { get; set; } = true;

    [Required]
    [Column("ORDER_NO")]
    public int ORDER_NO { get; set; }

    [ForeignKey("MENU_GROUP_ID")]
    public MENUGROUP MENUGROUP { get; set; } = null!;
}
