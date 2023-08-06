using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Entities
{
    [Table("user")]
    [Index(nameof(FullName), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; } = Gender.Male;

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must greater than 4 and less than 20 character")]        
        public string Password { get; set; }

        [Required(ErrorMessage = "Role can not be null")]
        public Role Role { get; set; } = Role.User;

        [Column("avatar_url")]
        public string? AvatarUrl { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

    }
}
