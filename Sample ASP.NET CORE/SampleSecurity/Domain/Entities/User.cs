using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string FullName { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public Gender Gender { get; set; } = Gender.Male;

        public Role Role { get; set; } = Role.User;

    }
}
