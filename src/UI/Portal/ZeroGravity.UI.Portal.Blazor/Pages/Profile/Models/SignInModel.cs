﻿using System.ComponentModel.DataAnnotations;

namespace ZeroGravity.UI.Portal.Blazor.Pages.Profile.Models;

public class SignInModel
{
    [Required]
    public string? UserName { get; set; }
 
    [Required]
    public string? Password { get; set; }
}
    