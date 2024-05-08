using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    [StringLength(30)] public string FullName { get; set; }

    [StringLength(10)]
    public string Gender { get; set; }
}