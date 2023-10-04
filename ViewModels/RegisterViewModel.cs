﻿using System.ComponentModel.DataAnnotations;

namespace PuhdasApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
