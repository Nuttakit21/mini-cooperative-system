using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Domain.Entities;

[Table("MENUGROUP")]
public class MENUGROUP
{
    [Key]
    [Column("MENU_GROUP_ID")]
    public int MENU_GROUP_ID { get; set; }

    [Required]
    [Column("APPLICATION_ID")]
    public int APPLICATION_ID { get; set; }

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

    [ForeignKey("APPLICATION_ID")]
    public APPLICATION APPLICATION { get; set; } = null!;

    public ICollection<MENUS> Menus { get; set; } = new List<MENUS>();
}
