using System.ComponentModel.DataAnnotations;

namespace V9.UI.Portal.Services.Authorization.Requests;

public class SignInRequest
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}