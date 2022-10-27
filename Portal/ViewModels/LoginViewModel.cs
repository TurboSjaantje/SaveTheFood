﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ViewModels;
public class LoginViewModel
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [UIHint("Password")]
    [PasswordPropertyText]
    public string Password { get; set; } = null!;
    public string ReturnUrl { get; set; } = "/Home/Index";
}
