using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sample.Entities
{
    [Table("user")]
    [Index(nameof(FullName), IsUnique = true)]
    public class User: BaseEntity
    {

        [Required(ErrorMessage = "Full name is required.")]
        //[Column("full_name")]
        public string FullName { get; set; }

        public Gender Gender { get; set; } = Gender.Male;

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must greater than 4 and less than 20 character")]        
        public string Password { get; set; }

        [Required(ErrorMessage = "Role can not be null")]
        public Role Role { get; set; } = Role.User;

        //[Column("avatar_url")]
        public string? AvatarUrl { get; set; }

    }
}
