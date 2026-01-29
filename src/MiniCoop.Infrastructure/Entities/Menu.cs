using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("menus")]
public class Menu
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(200)]
    public string Path { get; set; } = null!;

    [MaxLength(100)]
    public string? Icon { get; set; }

    [Required, MaxLength(50)]
    public string RequiredRole { get; set; } = null!;

    public int OrderNo { get; set; } = 0;

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; } = 0;

    public ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
}