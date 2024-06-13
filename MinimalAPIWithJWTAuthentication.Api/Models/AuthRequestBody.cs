using System.ComponentModel.DataAnnotations;
namespace MinimalAPIWithJWTAuthentication.Api.Models;
public class AuthRequestBody
{
    [Required]
    public string Username { get; set; } 
    [Required]
    public string Password { get; set; } 
}
