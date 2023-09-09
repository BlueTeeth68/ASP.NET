using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ReturnLoginUserDTO
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Username { get; set; }    
        public string Role { get; set; }    
        public string Gender { get; set; }  
        public string Token { get; set; }
    }
}
