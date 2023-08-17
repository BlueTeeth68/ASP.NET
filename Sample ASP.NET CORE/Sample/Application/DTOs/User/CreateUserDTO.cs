using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public record CreateUserDTO
    {
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; init; }

        public string? Gender { get; init; } = "Male";

        [Required(ErrorMessage = "Username is required.")]
        public string Username{ get; init; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password length must greater than 4 and less than 20 character")]
        public string Password { get; init; }

        //[Column("avatar_url")]
        //public string? AvatarUrl { get; init; }

    }
}
