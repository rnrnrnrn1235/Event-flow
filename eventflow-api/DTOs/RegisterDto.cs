using System.ComponentModel.DataAnnotations;
using Eventflow.Models.Enums;

public class RegisterDto
{
    [Required] public string username{get; set;}
    [Required, EmailAddress] public string email { get; set; }
    [Required, MinLength(6)] public string password { get; set; }
    [Required] public UserRole role { get; set;}
}