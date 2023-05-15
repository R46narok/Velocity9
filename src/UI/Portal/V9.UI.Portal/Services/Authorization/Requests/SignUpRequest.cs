using System.ComponentModel.DataAnnotations;

namespace V9.UI.Portal.Services.Authorization.Requests;

public class SignUpRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required] public string UserName { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>])(?=.*[a-zA-Z\d!@#$%^&*()\-_=+{};:,<.>]).{6,}$",
        ErrorMessage = "Password must be at least 6 characters long, and contain one lowercase letter, one uppercase letter, one digit, and one special character.")]
    public string Password { get; set; }
}