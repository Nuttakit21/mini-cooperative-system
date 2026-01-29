using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCoop.Infrastructure.Entities;

[Table("members")]
public class Member
{
    [Key]
    [Required, MaxLength(10)]
    public string MemberNo { get; set; } = null!;

    [Required, MaxLength(200)]
    public string FullName { get; set; } = null!;

    [Required, MaxLength(13)]
    public string IdCard { get; set; } = null!;

    [Required, MaxLength(20)]
    public string Status { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal AccountBalance { get; set; }

    public DateTime OpenDate { get; set; }
}