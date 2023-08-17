using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public record UserDTO
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; }

        public String Gender { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Role can not be null")]
        public String Role { get; set; }

        //[Column("avatar_url")]
        public string? AvatarUrl { get; set; }
    }
}
