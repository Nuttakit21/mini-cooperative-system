using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("users")]
public class Users
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [MaxLength(200)]
    public string? FullName { get; set; }

    [MaxLength(20)]
    public string Role { get; set; } = "User";

    public DateTime? CreatedAt { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }
}