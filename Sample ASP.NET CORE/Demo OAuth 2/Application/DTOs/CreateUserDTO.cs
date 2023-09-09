using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CreateUserDTO
    {
        [Required]
        public string Username { get; init; }

        [Required]
        public string FullName { get; init; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; init; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; init; } = Gender.Male;

        [EnumDataType(typeof(Role))]
        public Role Role { get; init; } = Role.User;

    }
}
