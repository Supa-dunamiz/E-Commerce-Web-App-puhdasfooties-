﻿namespace PuhdasApp.ViewModels
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
