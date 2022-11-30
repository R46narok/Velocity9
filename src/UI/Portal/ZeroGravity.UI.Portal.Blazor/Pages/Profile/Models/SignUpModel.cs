using System.ComponentModel.DataAnnotations;

namespace ZeroGravity.UI.Portal.Blazor.Pages.Profile.Models;

public class SignUpModel
{ 
    [Required] 
    public string? Email { get; set; }
 
    [Required]
    public string? UserName { get; set; }
 
    [Required]
    public string? Password { get; set; }
}