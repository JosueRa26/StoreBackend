using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreBackend.Domain.Entities;

[Table("User")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; private set; }

    [Required]
    public Guid ExternalId { get; set; }

    [Column("UserName")]
    [StringLength(50)]
    public string? UserName { get; set; }

    [Column("Email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("PasswordHash")]
    [StringLength(256)]
    public string? PasswordHash { get; set; } = string.Empty;
}