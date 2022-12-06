using System.ComponentModel.DataAnnotations;

namespace ZeroGravity.UI.Portal.Services.Authorization.Requests;

public class SignUpRequest
{
    [Required] public string Email { get; set; }

    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}