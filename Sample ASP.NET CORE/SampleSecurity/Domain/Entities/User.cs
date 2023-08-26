using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {

        public int Id { get; set; } 

        public string FullName { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public Gender Gender { get; set; } = Gender.Male;

        public Role Role { get; set; } = Role.User;

    }
}
