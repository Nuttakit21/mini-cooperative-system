using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("menu_roles")]
public class MenuRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int MenuId { get; set; }

    [Required, MaxLength(20)]
    public string Role { get; set; } = null!;

    [ForeignKey(nameof(MenuId))]
    public Menu Menu { get; set; } = null!;
}